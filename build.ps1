nuitka --onefile `
    --output-dir=build `
    --jobs=8 --disable-console `
    --enable-plugin=tk-inter `
    --assume-yes-for-downloads `
    .\Gui-src\main.py