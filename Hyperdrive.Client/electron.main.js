const { app, BrowserWindow } = require('electron/main')
const path = require('path');

const CreateWindow = () => {

    const isDev = !app.isPackaged;

    const win = new BrowserWindow({
        fullscreen: true,
        webPreferences: {
            nodeIntegration: false,
            contextIsolation: true,
        },
    });

    const mainUrl = isDev
        ? 'https://localhost:4200'
        : path.join(__dirname, 'dist', 'hyperdrive.client', 'browser', 'index.html');

    const LoadContent = () => {
        isDev ? win.loadURL(mainUrl) : win.loadFile(mainUrl);
    };

    LoadContent();

    win.webContents.on('did-fail-load', LoadContent);

    if (isDev) win.webContents.openDevTools();
}


app.whenReady().then(() => {
    CreateWindow();

    app.on('activate', () => {
        if (BrowserWindow.getAllWindows().length === 0) CreateWindow();
    });
});


app.on('window-all-closed', () => {
    if (process.platform !== 'darwin') app.quit();
})