import * as path from "path";

import { NextPage } from "next";

import { getFiles, FilePath } from "@/models/files";
import { createMetadata } from "@/models/meta";
import FileListItem from "@/components/FileListItem";

export const metadata = createMetadata({
	title: "XML"
});

interface StaticProps {
	path: string;
	files: Array<Readonly<FilePath>>;
}

const IntellisenseItemsPage: NextPage = async () => {
	const data = await getData();
	return (
		<ul>
			{data.files.map(a => (
				<li key={a.link} >
					<FileListItem {...a} />
				</li>
			))}
		</ul>
	)
};

export default IntellisenseItemsPage;

async function getData(): Promise<StaticProps> {
	var targetDirectoryPath = path.join(__dirname, "..", "..", "..", "..", "public", "data", "intellisense");
	const files = getFiles(targetDirectoryPath, 'data/intellisense', new Set(['.xml']));

	return {
		path: targetDirectoryPath,
		files: files,
	};
}

