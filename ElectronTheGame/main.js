const { app, BrowserWindow } = require('electron')

function createWindow () {
    // Create the browser window.
    const window = new BrowserWindow({
        fullscreen : true,
        webPreferences: {
            nodeIntegration: true
        }
    })

    // and load the main.html of the app.
    window.loadFile('main.html')
    //window.maximize();
}

// This method will be called when Electron has finished
// initialization and is ready to create browser windows.
// Some APIs can only be used after this event occurs.
app.whenReady().then(createWindow)

// Quit when all windows are closed.
app.on('window-all-closed', () => {
    app.quit()
})