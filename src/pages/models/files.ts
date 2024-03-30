import * as fs from "fs";
import * as path from "path";

export interface FilePath {
	relative: string;
	display: string;
	link: string;
	size: number;
}

export function getFiles(baseDirectory: string, basePath: string, extensions: ReadonlySet<string>): Array<Readonly<FilePath>> {
	const files = fs.readdirSync(baseDirectory, {
		recursive: true,
		withFileTypes: true,
	})
		.filter(a => a.isFile())
		.filter(a => extensions.has(path.extname(a.name)))
		.map(a => path.join(a.path, a.name))
		.map(a => {
			const relativePath = path.relative(baseDirectory, a);
			const stat = fs.statSync(a);

			return {
				relative: relativePath,
				display: basePath + '/' + relativePath.replaceAll('\\', '/'),
				link: '/' + basePath + '/' + relativePath.replaceAll('\\', '/'),
				size: stat.size,
			} satisfies FilePath;
		})
		;

	return files;
}
