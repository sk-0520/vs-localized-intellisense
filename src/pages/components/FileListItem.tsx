import Link from "next/link";
import type { FC } from "react";

import * as bytes from "bytes";

import type { FilePath } from "@/models/files";

const FileListItem: FC<FilePath> = (props: FilePath) => {
	return (
		<>
			<Link href={props.link}>{props.display}</Link>
			<span title={`${props.size.toLocaleString()} byte`}>
				({bytes.default(props.size)})
			</span>
		</>
	);
};

export default FileListItem;
