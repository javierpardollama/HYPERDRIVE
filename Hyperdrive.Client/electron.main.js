const { app, screen, BrowserWindow } = require('electron/main')
const path = require('path');

const CreateWindow = () => {
    const isdev = !app.isPackaged;

    const size = screen.getPrimaryDisplay().workAreaSize;

    const win = new BrowserWindow({
        x: 0,
        y: 0,
        width: size.width,
        height: size.height,
        backgroundColor:'#3f51b5',
        webPreferences: {
            nodeIntegration: false,
            contextIsolation: true,
        },
    });

    const mainurl = isdev
        ? 'https://localhost:4200'
        : path.join(__dirname, 'dist', 'hyperdrive.client', 'browser', 'index.html');

    const LoadContent = () => {
        isdev ? win.loadURL(mainurl) : win.loadFile(mainurl);
    };

    LoadContent();

    win.webContents.on('did-fail-load', LoadContent);

    if (isdev) win.webContents.openDevTools();
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
