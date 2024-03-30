import * as fs from "fs";
import * as path from "path";

const baseDirectoryPath = path.join(__dirname, '..', 'public', 'data', 'intellisense');

function createList(baseDirectory: string) {
	const items = fs.readdirSync(baseDirectory, {
		withFileTypes: true,
	});
	const dirs = items.filter(a => a.isDirectory());
	const files = items.filter(a => a.isFile() && a.name !== 'list.json');

	for (const dir of dirs) {
		const dirPath = path.resolve(dir.path, dir.name);
		createList(dirPath);
	}

	const listJsonPath = path.join(baseDirectory, "list.json");

	const data = {
		directory: dirs.map(a => a.name),
		file: files.map(a => a.name),
	};

	fs.writeFileSync(listJsonPath, JSON.stringify(data, undefined, "  "));
}

function main() {
	createList(baseDirectoryPath)
}

main();
