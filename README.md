## SharpKey
### Description
A library for creating ***simple*** and ***easy to use*** hotkeys.

### How To Use
Now obviously, this is more easily used in an application that uses the `System.Windows.Forms` library because of the WndProc(ref Message m) function.
```csharp
// Assuming you're in the code of a Windows Form.

private Hotkey Hotkey1, Hotkey2;

protected override void OnLoad(EventArgs e)
{
    base.OnLoad(e);
    
    // Initialize 2 new instances of Hotkey.
    Hotkey1 = new Hotkey(Handle, 0, FSModifiers.Shift, Keys.C);
    Hotkey2 = new Hotkey(Handle, 1, FSModifiers.Shift, Keys.M);
    
    // Register both instances.
    // The default value of the Notify parameter is set to TRUE, so you'll
    // notice immediately whether or not the registration was a success.
    Hotkey1.Hook();
    Hotkey2.Hook();
}

protected override void WndProc(ref Message m)
{
    if (m.Msg == Hotkey.HOTKEY)
    {
        int WParam = (int)m.WParam;
        
        if (WParam == Hotkey1.Id) // Close the window if Shift+C is pressed.
            Close();
        else if (WParam == Hotkey2.Id) // Minimize the window if Shift+M is pressed.
            WindowState = FormWindowState.Minimized;
    }
    
    base.WndProc(ref m);
}
```

### Download
[SharpKey.dll](https://github.com/Lexz-08/SharpKey/releases/download/sharpkey/SharpKey.dll)
