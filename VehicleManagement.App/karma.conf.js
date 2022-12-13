module.exports = function (config) {
  config.set({
    basePath: '',
    frameworks: ['jasmine', '@angular-devkit/build-angular'],
    plugins: [
      require('karma-jasmine'),
      require('karma-chrome-launcher'),
      require('karma-jasmine-html-reporter'),
      require('karma-coverage'),
      require('@angular-devkit/build-angular/plugins/karma'),
      require('karma-junit-reporter')
    ],
    client: {
      jasmine: {
        failSpecWithNoExpectations: true,
      },
      clearContext: false
    },
    jasmineHtmlReporter: {
      suppressAll: true
    },
    coverageReporter: {
      dir: require('path').join(__dirname, './coverage/vehicle-management-app'),
      subdir: '.',
      reporters: [
        { type: 'html' },
        { type: 'text-summary' }
      ]
    },
    junitReporter: {
      outputDir: 'testresults/junit',
      outputFile: 'unit-test-result.xml',
      useBrowserName: false
    },
    coverageReporter: {
      include: [
        // Specify include pattern(s) first
        'src/**/*.(ts|js)',
        // Then specify "do not touch" patterns
        '!src/main.(ts|js)',
        '!src/**/*.spec.(ts|js)',
        '!src/**/*.mock.(ts|js)',
        '!src/**/*.module.(ts|js)',
        '!src/**/environment*.(ts|js)',
        '!src/**/*.interface*.(ts|js)',
        '!src/**/*.enum*.(ts|js)',
        '!test/**/*.(ts|js)',
      ],
      type : 'cobertura',
      dir : 'testresults',
      subdir:'coverage',
      file: 'code-coverage.xml'
    },
    reporters: ['progress', 'kjhtml', 'coverage', 'junit'],
    port: 9876,
    colors: true,
    logLevel: config.LOG_INFO,
    autoWatch: true,
    browsers: ['Chrome', 'ChromeHeadlessNoSandbox'],
    customLaunchers: {
      ChromeHeadlessNoSandbox: {
        base: "Chrome",
        flags: [
          "--no-sandbox",
          "--headless",
          "--user-data-dir=/tmp/chrome-test-profile",
          "--disable-web-security",
          "--remote-debugging-address=0.0.0.0",
          "--remote-debugging-port=9222",
        ],
        debug: true,
      },
    },
    singleRun: false,
    restartOnFileChange: true
  });
};
