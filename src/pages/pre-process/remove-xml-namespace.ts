import * as fs from "fs";
import * as fs_async from "fs/promises";
import * as path from "path";

import * as xmldoc from "xmldoc";

//const namespace = "https://github.com/sk-0520/vs-localized-intellisense/tree/master/schema/2024";
const xml = {
	prefix: "vsli",
	attribute: {
		raw: 'raw',
	},
}

const baseDirectoryPath = path.join(__dirname, '..', 'public', 'data', 'intellisense');

const updateTargetDirectoryNames: ReadonlySet<string> = new Set([
	'netstandard2.1',
	'net9.0',
]);


function getElements(parent: xmldoc.XmlElement): xmldoc.XmlElement[] {
	const childElements = parent.children
		.filter(a => a.type === 'element')
		.map(a => a as xmldoc.XmlElement)
		;

	const result = childElements;

	for (const childElement of childElements) {
		const elements = getElements(childElement);
		for (const element of elements) {
			result.push(element);
		}
	}

	return result;
}

function getTopLevelSubDirectories(directoryPath: string): string[] {
	return fs.readdirSync(directoryPath, {
		recursive: false,
		withFileTypes: true,
	})
		.filter(a => a.isDirectory())
		.map(a => path.join(a.path, a.name))
		;
}

async function getIntellisenseFiles(directoryPath: string): Promise<string[]> {
	const files = await fs_async.readdir(directoryPath, {
		recursive: true,
		withFileTypes: true,
	})

	return files
		.filter(a => a.isFile())
		.filter(a => path.extname(a.name) === ".xml")
		.map(a => path.join(a.path, a.name))
		;
}

async function updateIntellisenseFiles(directoryPath: string): Promise<void> {
	const dirName = path.basename(directoryPath);
	const files = await getIntellisenseFiles(directoryPath);

	let counter = 0;
	for (const file of files) {
		const percent = (++counter / files.length) * 100.0;
		console.info(`[${dirName}] ${percent.toFixed(2)}%, ${file}`);

		const xmlData = await fs_async.readFile(file);
		const xmlText = xmlData.toString();

		var document = new xmldoc.XmlDocument(xmlText);

		const elements = getElements(document);
		for (const element of elements) {
			delete element.attr[`${xml.prefix}:${xml.attribute.raw}`];
		}
		delete document.attr[`xmlns:${xml.prefix}`];

		const content = document.toString({ compressed: true });

		await fs_async.writeFile(file, content);
	}
}

(async () => {
	const dirs = getTopLevelSubDirectories(baseDirectoryPath);

	const promiseItems: Array<Promise<void>> = [];
	for (const dir of dirs) {
		const dirName = path.basename(dir);
		if (updateTargetDirectoryNames.has(dirName)) {
			const promise = updateIntellisenseFiles(dir);
			promiseItems.push(promise);
		}
	}

	await Promise.all(promiseItems);
})();

