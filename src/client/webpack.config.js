const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

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
				use: [{
					//loader: 'style-loader', // inject CSS to page
					loader: MiniCssExtractPlugin.loader,
					options: {
						path: path.resolve(__dirname, "dist"),
					}
				}, {
					loader: 'css-loader', // translates CSS into CommonJS modules
				}, {
					loader: 'sass-loader' // compiles Sass to CSS
				}]
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
		}),
		new MiniCssExtractPlugin({
      // Options similar to the same options in webpackOptions.output
      // both options are optional
      filename: "[name].css",
      chunkFilename: "[id].css"
    })
  ]
}