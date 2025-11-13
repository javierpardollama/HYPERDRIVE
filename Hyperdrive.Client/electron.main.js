const { app, screen, BrowserWindow } = require('electron/main')
const path = require('path');

const CreateWindow = () => {
    const size = screen.getPrimaryDisplay().workAreaSize;

    const win = new BrowserWindow({
        x: 0,
        y: 0,
        width: size.width,
        height: size.height,
        webPreferences: {
            nodeIntegration: false,
            contextIsolation: true,
        },
    });

    const isdev = !app.isPackaged;

    const mainurl = isisdevDev
        ? 'https://localhost:4200'
        : path.join(__dirname, 'dist', 'hyperdrive.client', 'browser', 'index.html');

    // Load initial content
    isdev ? win.loadURL(mainurl) : win.loadFile(mainurl);

    // Retry on fail
    win.webContents.on('did-fail-load', () => {
        isdev ? win.loadURL(mainurl) : win.loadFile(mainurl);
    });

    // Open DevTools in development
    if (isdev) {
        win.webContents.openDevTools();
    }
}

app.on('ready', () => {
    CreateWindow()

    app.on('activate', () => {
        if (BrowserWindow.getAllWindows().length === 0) {
            CreateWindow()
        }
    })
})

app.on('window-all-closed', () => {
    if (process.platform !== 'darwin') {
        app.quit()
    }
})