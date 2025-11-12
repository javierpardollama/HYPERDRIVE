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

    if (!app.isPackaged) {
        win.loadURL('https://localhost:4200');
        win.webContents.on('did-fail-load', () => win.loadURL('https://localhost:4200'));
        win.webContents.openDevTools();
    } else{
        win.loadFile(path.join(__dirname, 'dist', 'hyperdrive.client', 'browser', 'index.html'));
        win.webContents.on('did-fail-load', () => win.loadFile(path.join(__dirname, 'dist', 'hyperdrive.client', 'browser', 'index.html')));
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