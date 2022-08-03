const TerserPlugin = require("terser-webpack-plugin");
const MergeIntoSingleFilePlugin = require('webpack-merge-and-include-globally');
const { merge } = require('webpack-merge');
const uglifyJS = require('uglify-js');

const commonConfig = {
    entry: [
        "./boilerplate.ui/assets/styles/vendor.scss",
        "./boilerplate.ui/assets/styles/main.scss"
    ],
    output: {
        path: __dirname + '/wwwroot'
    },
    plugins: [
        new MergeIntoSingleFilePlugin({
            files: [{
                src: [
                    'node_modules/jquery/dist/jquery.js',
                    'node_modules/jquery-validation/dist/jquery.validate.js',
                    'node_modules/jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.js',
                    'node_modules/bootstrap/dist/js/bootstrap.js',
                ],
                dest: code => {
                    const min = uglifyJS.minify(code, {
                        sourceMap: {
                            filename: 'vendor.js',
                            url: 'vendor.js.map'
                        }
                    });
                    return {
                        '/js/vendor.js': min.code,
                        '/js/vendor.js.map': min.map
                    }
                },
            },
            {
                src: [
                    './boilerplate.ui/assets/js/site.js',
                ],
                dest: code => {
                    const min = uglifyJS.minify(code, {
                        sourceMap: {
                            filename: 'main.js',
                            url: 'main.js.map'
                        }
                    });
                    return {
                        '/js/main.js': min.code,
                        '/js/main.js.map': min.map
                    }
                },
            }],
        }),
    ]
};

const developmentConfig = {
    mode: 'development',
    devtool: 'source-map',
    module: {
        rules: [
            {
                test: /\.scss$/,
                use: [
                    {
                        loader: 'file-loader',
                        options: {
                            name: '/css/[name].css',
                        }
                    },
                    {
                        loader: 'extract-loader',
                        options: {
                            esModule: false,
                        }
                    },
                    {
                        loader: 'css-loader',
                        options: {
                            sourceMap: true,
                        },
                    },
                    {
                        loader: 'postcss-loader',
                        options: {
                            postcssOptions: {
                                ident: "postcss",
                                sourceMap: true,
                                plugins: () => [
                                    require("cssnano")({
                                        preset: [
                                            "default",
                                            {
                                                discardComments: {
                                                    removeAll: true,
                                                },
                                            },
                                        ],
                                    }),
                                ]
                            },
                        },
                    },
                    {
                        loader: 'sass-loader',
                        options: {
                            sourceMap: true,
                        },
                    }
                ]
            },
            {
                test: /\.(woff|woff2|eot|ttf|otf)$/,
                use: [
                    'file-loader',
                ],
            },
        ]
    },
    optimization: {
        minimizer: [
            new TerserPlugin({
                sourceMap: true,
                extractComments: true,
            }),
        ],
    },
};

const productionConfig = {
    mode: 'production',
    module: {
        rules: [
            {
                test: /\.scss$/,
                use: [
                    {
                        loader: 'file-loader',
                        options: {
                            name: '/css/[name].css',
                        }
                    },
                    {
                        loader: 'extract-loader',
                        options: {
                            esModule: false,
                        }
                    },
                    {
                        loader: 'css-loader'
                    },
                    {
                        loader: 'postcss-loader',
                        options: {
                            options: {},
                        }
                    },
                    {
                        loader: 'sass-loader'
                    }
                ]
            },
            {
                test: /\.(woff|woff2|eot|ttf|otf)$/,
                use: [
                    'file-loader',
                ],
            },
        ]
    },
    optimization: {
        minimize: true,
        minimizer: [new TerserPlugin({
            test: /\.m?js$/,
        })],
    },
};

module.exports = env => {
    if(env.development) 
        return merge(commonConfig, developmentConfig);
	else if(env.production)
			return merge(commonConfig, productionConfig);
        else
            throw new Error('No matching configuration was found! ' +JSON.stringify(env));
}