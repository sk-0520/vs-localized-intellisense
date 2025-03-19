import * as fs from "fs";
import * as path from "path";

const rootDirectoryPath = path.resolve(__dirname, "..", "..", "..");
const srcDirectoryPath = path.join(rootDirectoryPath, "work-intellisense");
const destDirectoryPath = path.join(
	rootDirectoryPath,
	"src",
	"pages",
	"public",
	"data",
	"intellisense",
);

fs.cpSync(srcDirectoryPath, destDirectoryPath, { recursive: true });
