import { Metadata } from "next";

export function createMetadata(meta: Metadata): Metadata {
	const base: Metadata = {
		title: "vs-localized-intellisense"
	} as const;

	const result = { ...base, ...meta };

	if (meta.title) {
		result.title = `${meta.title} - ${base.title}`
	}

	return result;
}
