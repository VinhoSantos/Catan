const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");

module.exports = {
	entry: "./src/scripts/app.ts",
	output: {
		path: path.resolve(__dirname, "dist"),
		filename: "bundle.js",
	},
	resolve: {
		extensions: [".ts", ".tsx", ".js"]
	},
    devServer: {
		contentBase: "./dist"
	},
	module: {
		rules: [
			{
				test: /\.ts?$/,
				exclude: /node_modules/,
				use: [
					{ loader: "ts-loader" }
				]
			},
            {
				test: /\.(s*)css$/,
				exclude: /node_modules/,
				use: [
					"style-loader",
                    "css-loader",
                    "sass-loader"
				]
			},
			{
				test: /\.mp3$/,
				exclude: /node_modules/,
				use: [
					{ loader: "file-loader" }
				]
			},
			{
				test: /\.(jp(e*)g|png|svg)$/,
				exclude: /node_modules/,
				use: [
					{ loader: "url-loader" }
				]
			}
		]
	},
	plugins: [
    new HtmlWebpackPlugin({
			template: "./src/index.html"
		})
  ],
}