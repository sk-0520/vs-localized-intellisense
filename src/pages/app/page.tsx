import { NextPage } from "next";
import Link from "next/link";
import { createMetadata } from "@/models/meta";

export const metadata = createMetadata({});

const HomePage: NextPage = () => {
	return (
		<ul>
			<li>
				data
				<ul>
					<li>
						<Link href="intellisense">intellisense</Link>
					</li>
					<li>
						<Link href="intellisense/json">intellisense:list-json</Link>
					</li>
				</ul>
			</li>
			<li>
				<Link href="https://github.com/sk-0520/vs-localized-intellisense">repository</Link>
			</li>
		</ul>
	);
};

export default HomePage;
