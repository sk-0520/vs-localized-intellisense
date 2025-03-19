const isProduction = process.env.NODE_ENV === "production";
const prefixPath = isProduction ? "/vs-localized-intellisense" : "";

/** @type {import('next').NextConfig} */
const nextConfig = {
	output: "export",
	basePath: prefixPath,
};

export default nextConfig;
