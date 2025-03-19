import * as fs from "node:fs";
import * as path from "node:path";

const rootDirectoryPath = path.resolve(__dirname, "..", "..", "..");
const srcIconDirectoryPath = path.join(rootDirectoryPath, "resources", "Icon");

const destIcoDirectoryPath = path.join(
	rootDirectoryPath,
	"src",
	"pages",
	"app",
);
const destSvgDirectoryPath = path.join(
	rootDirectoryPath,
	"src",
	"pages",
	"public",
);

fs.mkdirSync(destIcoDirectoryPath, { recursive: true });
fs.copyFileSync(
	path.join(srcIconDirectoryPath, "main.ico"),
	path.join(destIcoDirectoryPath, "favicon.ico"),
);

fs.mkdirSync(destSvgDirectoryPath, { recursive: true });
fs.copyFileSync(
	path.join(srcIconDirectoryPath, "main.svg"),
	path.join(destSvgDirectoryPath, "favicon.svg"),
);
