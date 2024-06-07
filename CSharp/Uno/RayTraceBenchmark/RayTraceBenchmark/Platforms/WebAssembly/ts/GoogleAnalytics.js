var Uno;
(function (Uno) {
    var UI;
    (function (UI) {
        var Demo;
        (function (Demo) {
            var Analytics = /** @class */ (function () {
                function Analytics() {
                }
                Analytics.reportPageView = function (screenName, appName) {
                    if (appName === void 0) { appName = "raytracer-benchmark-wasm"; }
                    if (Analytics.init(screenName, appName)) {
                        return "ok";
                    }
                    var gtag = window.gtag;
                    if (gtag) {
                        gtag("event", "screen_view", {
                            screen_name: screenName,
                            app_name: appName
                        });
                    }
                    else {
                        console.error("Google Analytics not present, can't report page view for ".concat(screenName, "."));
                    }
                    return "ok";
                };
                Analytics.init = function (screenName, appName) {
                    if (Analytics.isLoaded) {
                        return false;
                    }
                    var script = "\n              window.dataLayer = window.dataLayer || [];\n              function gtag() { dataLayer.push(arguments); }\n              gtag('js', new Date());\n              gtag('config', 'UA-26688675-10');\n\n              gtag(\"event\", \"screen_view\", {screen_name: \"".concat(screenName, "\", app_name: \"").concat(appName, "\"});");
                    var script1 = document.createElement("script");
                    script1.type = "text/javascript";
                    script1.src = "https://www.googletagmanager.com/gtag/js?id=UA-26688675-10";
                    document.body.appendChild(script1);
                    var script2 = document.createElement("script");
                    script2.type = "text/javascript";
                    script2.innerText = script;
                    document.body.appendChild(script2);
                    Analytics.isLoaded = true;
                    return true;
                };
                Analytics.isLoaded = false;
                return Analytics;
            }());
            Demo.Analytics = Analytics;
        })(Demo = UI.Demo || (UI.Demo = {}));
    })(UI = Uno.UI || (Uno.UI = {}));
})(Uno || (Uno = {}));
Uno.UI.Demo.Analytics.reportPageView("init");
//# sourceMappingURL=GoogleAnalytics.js.map