// const gulp = require("gulp");
// const concat = require("gulp-concat");
// const cleanCss = require("gulp-clean-css");
// const del = require("del");

import gulp from "gulp";
import concat from "gulp-concat";
import cleanCss from "gulp-clean-css";
import { deleteAsync } from "del";
import browserSync from "browser-sync";
import uglify from "gulp-uglify";

const browsersync = browserSync.create();

// Concat and minify CSS files
gulp.task("build-css", () => {
  return gulp
    .src("wwwroot/css/*.css")
    .pipe(concat("demosite.min.css"))
    .pipe(cleanCss())
    .pipe(gulp.dest("wwwroot/"));
});

gulp.task("build-js", () => {
  return gulp
    .src([
      "wwwroot/js/jquery-3.6.0.min.js",
      "wwwroot/js/moment-with-locales.js",
      "wwwroot/js/selectors.js",
      "wwwroot/js/utils.js",
      "wwwroot/js/site.js",
      "wwwroot/js/main.js",
      "wwwroot/js/slick.min.js",
      "wwwroot/js/banner-slider.js",
      "wwwroot/js/news-slider.js",
      "wwwroot/js/tabs-card-slider.js",
      "wwwroot/js/form-validate.js",
      "wwwroot/js/jquery.validate.min.js",
      "wwwroot/js/subscribe.js",
      "wwwroot/js/highstock.js",
      "wwwroot/js/stock-helper.js",
      "wwwroot/js/stock-details.js",
      "wwwroot/js/stock-shares-chart.js",
      "wwwroot/js/stock-summary-chart.js",
      "wwwroot/js/shareholder-chart.js",
      "wwwroot/js/fancybox.umd.js",
      "wwwroot/js/event-slider.js",
      "wwwroot/js/report-slider.js",
      "wwwroot/js/feedback-form.js",
      "wwwroot/js/search/autoComplete.js",
      "wwwroot/js/search/search-on-results.js",
    ])
    .pipe(concat("demosite.min.js"))
    .pipe(uglify())
    .pipe(gulp.dest("wwwroot/"));
});

gulp.task("clean", async () => {
  return deleteAsync(["wwwroot/demosite.min.css", "wwwroot/demosite.min.js"]);
});

gulp.task("session-start", (cb) => {
  return gulp.series("clean", "build-css", "build-js")(cb);
});

gulp.task("server", () => {
  browsersync.init({
    proxy: "https://localhost:5001/",
    watch: true,
  });

  gulp.watch(
    "wwwroot/css/*.css",
    gulp.series("session-start"),
    browsersync.reload
  );

  gulp.watch(
    ["wwwroot/js/**/*.js", "wwwroot/lib/**/*.js"],
    gulp.series("session-start"),
    browsersync.reload
  );
});

gulp.task("default", gulp.series("session-start", "server"));
