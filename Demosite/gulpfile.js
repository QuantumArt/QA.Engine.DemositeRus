import gulp from "gulp";
import concat from "gulp-concat";
import cleanCss from "gulp-clean-css";
import { deleteAsync } from "del";
import browserSync from "browser-sync";
import uglify from "gulp-uglify";
import fs from "fs";

const browsersync = browserSync.create();

const requiredJsFilesList = JSON.parse(
  fs.readFileSync("bundleconfig.json")
)[1];

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
    .src(requiredJsFilesList.inputFiles)
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
    ["wwwroot/js/**/*.js"],
    gulp.series("session-start"),
    browsersync.reload
  );
});

gulp.task("default", gulp.series("session-start", "server"));
