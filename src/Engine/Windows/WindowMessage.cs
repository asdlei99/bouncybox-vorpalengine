using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

#pragma warning disable 1591

namespace BouncyBox.VorpalEngine.Engine.Windows
{
    /// <summary>Maps common window messages to string representations of their constants.</summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class WindowMessage
    {
        public static readonly WindowMessage MN_GETHMENU = new WindowMessage(nameof(MN_GETHMENU), TerraFX.Interop.User32.MN_GETHMENU);
        public static readonly WindowMessage WM_ACTIVATE = new WindowMessage(nameof(WM_ACTIVATE), TerraFX.Interop.User32.WM_ACTIVATE);
        public static readonly WindowMessage WM_ACTIVATEAPP = new WindowMessage(nameof(WM_ACTIVATEAPP), TerraFX.Interop.User32.WM_ACTIVATEAPP);
        public static readonly WindowMessage WM_AFXFIRST = new WindowMessage(nameof(WM_AFXFIRST), TerraFX.Interop.User32.WM_AFXFIRST);
        public static readonly WindowMessage WM_AFXLAST = new WindowMessage(nameof(WM_AFXLAST), TerraFX.Interop.User32.WM_AFXLAST);
        public static readonly WindowMessage WM_APP = new WindowMessage(nameof(WM_APP), TerraFX.Interop.User32.WM_APP);
        public static readonly WindowMessage WM_APPCOMMAND = new WindowMessage(nameof(WM_APPCOMMAND), TerraFX.Interop.User32.WM_APPCOMMAND);
        public static readonly WindowMessage WM_ASKCBFORMATNAME = new WindowMessage(nameof(WM_ASKCBFORMATNAME), TerraFX.Interop.User32.WM_ASKCBFORMATNAME);
        public static readonly WindowMessage WM_CANCELJOURNAL = new WindowMessage(nameof(WM_CANCELJOURNAL), TerraFX.Interop.User32.WM_CANCELJOURNAL);
        public static readonly WindowMessage WM_CANCELMODE = new WindowMessage(nameof(WM_CANCELMODE), TerraFX.Interop.User32.WM_CANCELMODE);
        public static readonly WindowMessage WM_CAPTURECHANGED = new WindowMessage(nameof(WM_CAPTURECHANGED), TerraFX.Interop.User32.WM_CAPTURECHANGED);
        public static readonly WindowMessage WM_CHANGECBCHAIN = new WindowMessage(nameof(WM_CHANGECBCHAIN), TerraFX.Interop.User32.WM_CHANGECBCHAIN);
        public static readonly WindowMessage WM_CHANGEUISTATE = new WindowMessage(nameof(WM_CHANGEUISTATE), TerraFX.Interop.User32.WM_CHANGEUISTATE);
        public static readonly WindowMessage WM_CHAR = new WindowMessage(nameof(WM_CHAR), TerraFX.Interop.User32.WM_CHAR);
        public static readonly WindowMessage WM_CHARTOITEM = new WindowMessage(nameof(WM_CHARTOITEM), TerraFX.Interop.User32.WM_CHARTOITEM);
        public static readonly WindowMessage WM_CHILDACTIVATE = new WindowMessage(nameof(WM_CHILDACTIVATE), TerraFX.Interop.User32.WM_CHILDACTIVATE);
        public static readonly WindowMessage WM_CLEAR = new WindowMessage(nameof(WM_CLEAR), TerraFX.Interop.User32.WM_CLEAR);
        public static readonly WindowMessage WM_CLIPBOARDUPDATE = new WindowMessage(nameof(WM_CLIPBOARDUPDATE), TerraFX.Interop.User32.WM_CLIPBOARDUPDATE);
        public static readonly WindowMessage WM_CLOSE = new WindowMessage(nameof(WM_CLOSE), TerraFX.Interop.User32.WM_CLOSE);
        public static readonly WindowMessage WM_COMMAND = new WindowMessage(nameof(WM_COMMAND), TerraFX.Interop.User32.WM_COMMAND);
        public static readonly WindowMessage WM_COMMNOTIFY = new WindowMessage(nameof(WM_COMMNOTIFY), TerraFX.Interop.User32.WM_COMMNOTIFY);
        public static readonly WindowMessage WM_COMPACTING = new WindowMessage(nameof(WM_COMPACTING), TerraFX.Interop.User32.WM_COMPACTING);
        public static readonly WindowMessage WM_COMPAREITEM = new WindowMessage(nameof(WM_COMPAREITEM), TerraFX.Interop.User32.WM_COMPAREITEM);
        public static readonly WindowMessage WM_CONTEXTMENU = new WindowMessage(nameof(WM_CONTEXTMENU), TerraFX.Interop.User32.WM_CONTEXTMENU);
        public static readonly WindowMessage WM_COPY = new WindowMessage(nameof(WM_COPY), TerraFX.Interop.User32.WM_COPY);
        public static readonly WindowMessage WM_COPYDATA = new WindowMessage(nameof(WM_COPYDATA), TerraFX.Interop.User32.WM_COPYDATA);
        public static readonly WindowMessage WM_CREATE = new WindowMessage(nameof(WM_CREATE), TerraFX.Interop.User32.WM_CREATE);
        public static readonly WindowMessage WM_CTLCOLORBTN = new WindowMessage(nameof(WM_CTLCOLORBTN), TerraFX.Interop.User32.WM_CTLCOLORBTN);
        public static readonly WindowMessage WM_CTLCOLORDLG = new WindowMessage(nameof(WM_CTLCOLORDLG), TerraFX.Interop.User32.WM_CTLCOLORDLG);
        public static readonly WindowMessage WM_CTLCOLOREDIT = new WindowMessage(nameof(WM_CTLCOLOREDIT), TerraFX.Interop.User32.WM_CTLCOLOREDIT);
        public static readonly WindowMessage WM_CTLCOLORLISTBOX = new WindowMessage(nameof(WM_CTLCOLORLISTBOX), TerraFX.Interop.User32.WM_CTLCOLORLISTBOX);
        public static readonly WindowMessage WM_CTLCOLORMSGBOX = new WindowMessage(nameof(WM_CTLCOLORMSGBOX), TerraFX.Interop.User32.WM_CTLCOLORMSGBOX);

        public static readonly WindowMessage WM_CTLCOLORSCROLLBAR = new WindowMessage(
            nameof(WM_CTLCOLORSCROLLBAR),
            TerraFX.Interop.User32.WM_CTLCOLORSCROLLBAR);

        public static readonly WindowMessage WM_CTLCOLORSTATIC = new WindowMessage(nameof(WM_CTLCOLORSTATIC), TerraFX.Interop.User32.WM_CTLCOLORSTATIC);
        public static readonly WindowMessage WM_CUT = new WindowMessage(nameof(WM_CUT), TerraFX.Interop.User32.WM_CUT);
        public static readonly WindowMessage WM_DEADCHAR = new WindowMessage(nameof(WM_DEADCHAR), TerraFX.Interop.User32.WM_DEADCHAR);
        public static readonly WindowMessage WM_DELETEITEM = new WindowMessage(nameof(WM_DELETEITEM), TerraFX.Interop.User32.WM_DELETEITEM);
        public static readonly WindowMessage WM_DESTROY = new WindowMessage(nameof(WM_DESTROY), TerraFX.Interop.User32.WM_DESTROY);
        public static readonly WindowMessage WM_DESTROYCLIPBOARD = new WindowMessage(nameof(WM_DESTROYCLIPBOARD), TerraFX.Interop.User32.WM_DESTROYCLIPBOARD);
        public static readonly WindowMessage WM_DEVICECHANGE = new WindowMessage(nameof(WM_DEVICECHANGE), TerraFX.Interop.User32.WM_DEVICECHANGE);
        public static readonly WindowMessage WM_DEVMODECHANGE = new WindowMessage(nameof(WM_DEVMODECHANGE), TerraFX.Interop.User32.WM_DEVMODECHANGE);
        public static readonly WindowMessage WM_DISPLAYCHANGE = new WindowMessage(nameof(WM_DISPLAYCHANGE), TerraFX.Interop.User32.WM_DISPLAYCHANGE);
        public static readonly WindowMessage WM_DPICHANGED = new WindowMessage(nameof(WM_DPICHANGED), TerraFX.Interop.User32.WM_DPICHANGED);

        public static readonly WindowMessage WM_DPICHANGED_AFTERPARENT = new WindowMessage(
            nameof(WM_DPICHANGED_AFTERPARENT),
            TerraFX.Interop.User32.WM_DPICHANGED_AFTERPARENT);

        public static readonly WindowMessage WM_DPICHANGED_BEFOREPARENT = new WindowMessage(
            nameof(WM_DPICHANGED_BEFOREPARENT),
            TerraFX.Interop.User32.WM_DPICHANGED_BEFOREPARENT);

        public static readonly WindowMessage WM_DRAWCLIPBOARD = new WindowMessage(nameof(WM_DRAWCLIPBOARD), TerraFX.Interop.User32.WM_DRAWCLIPBOARD);
        public static readonly WindowMessage WM_DRAWITEM = new WindowMessage(nameof(WM_DRAWITEM), TerraFX.Interop.User32.WM_DRAWITEM);
        public static readonly WindowMessage WM_DROPFILES = new WindowMessage(nameof(WM_DROPFILES), TerraFX.Interop.User32.WM_DROPFILES);

        public static readonly WindowMessage WM_DWMCOLORIZATIONCOLORCHANGED = new WindowMessage(
            nameof(WM_DWMCOLORIZATIONCOLORCHANGED),
            TerraFX.Interop.User32.WM_DWMCOLORIZATIONCOLORCHANGED);

        public static readonly WindowMessage WM_DWMCOMPOSITIONCHANGED = new WindowMessage(
            nameof(WM_DWMCOMPOSITIONCHANGED),
            TerraFX.Interop.User32.WM_DWMCOMPOSITIONCHANGED);

        public static readonly WindowMessage WM_DWMNCRENDERINGCHANGED = new WindowMessage(
            nameof(WM_DWMNCRENDERINGCHANGED),
            TerraFX.Interop.User32.WM_DWMNCRENDERINGCHANGED);

        public static readonly WindowMessage WM_DWMSENDICONICLIVEPREVIEWBITMAP = new WindowMessage(
            nameof(WM_DWMSENDICONICLIVEPREVIEWBITMAP),
            TerraFX.Interop.User32.WM_DWMSENDICONICLIVEPREVIEWBITMAP);

        public static readonly WindowMessage WM_DWMSENDICONICTHUMBNAIL = new WindowMessage(
            nameof(WM_DWMSENDICONICTHUMBNAIL),
            TerraFX.Interop.User32.WM_DWMSENDICONICTHUMBNAIL);

        public static readonly WindowMessage WM_DWMWINDOWMAXIMIZEDCHANGE = new WindowMessage(
            nameof(WM_DWMWINDOWMAXIMIZEDCHANGE),
            TerraFX.Interop.User32.WM_DWMWINDOWMAXIMIZEDCHANGE);

        public static readonly WindowMessage WM_ENABLE = new WindowMessage(nameof(WM_ENABLE), TerraFX.Interop.User32.WM_ENABLE);
        public static readonly WindowMessage WM_ENDSESSION = new WindowMessage(nameof(WM_ENDSESSION), TerraFX.Interop.User32.WM_ENDSESSION);
        public static readonly WindowMessage WM_ENTERIDLE = new WindowMessage(nameof(WM_ENTERIDLE), TerraFX.Interop.User32.WM_ENTERIDLE);
        public static readonly WindowMessage WM_ENTERMENULOOP = new WindowMessage(nameof(WM_ENTERMENULOOP), TerraFX.Interop.User32.WM_ENTERMENULOOP);
        public static readonly WindowMessage WM_ENTERSIZEMOVE = new WindowMessage(nameof(WM_ENTERSIZEMOVE), TerraFX.Interop.User32.WM_ENTERSIZEMOVE);
        public static readonly WindowMessage WM_ERASEBKGND = new WindowMessage(nameof(WM_ERASEBKGND), TerraFX.Interop.User32.WM_ERASEBKGND);
        public static readonly WindowMessage WM_EXITMENULOOP = new WindowMessage(nameof(WM_EXITMENULOOP), TerraFX.Interop.User32.WM_EXITMENULOOP);
        public static readonly WindowMessage WM_EXITSIZEMOVE = new WindowMessage(nameof(WM_EXITSIZEMOVE), TerraFX.Interop.User32.WM_EXITSIZEMOVE);
        public static readonly WindowMessage WM_FONTCHANGE = new WindowMessage(nameof(WM_FONTCHANGE), TerraFX.Interop.User32.WM_FONTCHANGE);
        public static readonly WindowMessage WM_GESTURE = new WindowMessage(nameof(WM_GESTURE), TerraFX.Interop.User32.WM_GESTURE);
        public static readonly WindowMessage WM_GESTURENOTIFY = new WindowMessage(nameof(WM_GESTURENOTIFY), TerraFX.Interop.User32.WM_GESTURENOTIFY);
        public static readonly WindowMessage WM_GETDLGCODE = new WindowMessage(nameof(WM_GETDLGCODE), TerraFX.Interop.User32.WM_GETDLGCODE);
        public static readonly WindowMessage WM_GETDPISCALEDSIZE = new WindowMessage(nameof(WM_GETDPISCALEDSIZE), TerraFX.Interop.User32.WM_GETDPISCALEDSIZE);
        public static readonly WindowMessage WM_GETFONT = new WindowMessage(nameof(WM_GETFONT), TerraFX.Interop.User32.WM_GETFONT);
        public static readonly WindowMessage WM_GETHOTKEY = new WindowMessage(nameof(WM_GETHOTKEY), TerraFX.Interop.User32.WM_GETHOTKEY);
        public static readonly WindowMessage WM_GETICON = new WindowMessage(nameof(WM_GETICON), TerraFX.Interop.User32.WM_GETICON);
        public static readonly WindowMessage WM_GETMINMAXINFO = new WindowMessage(nameof(WM_GETMINMAXINFO), TerraFX.Interop.User32.WM_GETMINMAXINFO);
        public static readonly WindowMessage WM_GETOBJECT = new WindowMessage(nameof(WM_GETOBJECT), TerraFX.Interop.User32.WM_GETOBJECT);
        public static readonly WindowMessage WM_GETTEXT = new WindowMessage(nameof(WM_GETTEXT), TerraFX.Interop.User32.WM_GETTEXT);
        public static readonly WindowMessage WM_GETTEXTLENGTH = new WindowMessage(nameof(WM_GETTEXTLENGTH), TerraFX.Interop.User32.WM_GETTEXTLENGTH);

        public static readonly WindowMessage WM_GETTITLEBARINFOEX = new WindowMessage(
            nameof(WM_GETTITLEBARINFOEX),
            TerraFX.Interop.User32.WM_GETTITLEBARINFOEX);

        public static readonly WindowMessage WM_HANDHELDFIRST = new WindowMessage(nameof(WM_HANDHELDFIRST), TerraFX.Interop.User32.WM_HANDHELDFIRST);
        public static readonly WindowMessage WM_HANDHELDLAST = new WindowMessage(nameof(WM_HANDHELDLAST), TerraFX.Interop.User32.WM_HANDHELDLAST);
        public static readonly WindowMessage WM_HELP = new WindowMessage(nameof(WM_HELP), TerraFX.Interop.User32.WM_HELP);
        public static readonly WindowMessage WM_HOTKEY = new WindowMessage(nameof(WM_HOTKEY), TerraFX.Interop.User32.WM_HOTKEY);
        public static readonly WindowMessage WM_HSCROLL = new WindowMessage(nameof(WM_HSCROLL), TerraFX.Interop.User32.WM_HSCROLL);
        public static readonly WindowMessage WM_HSCROLLCLIPBOARD = new WindowMessage(nameof(WM_HSCROLLCLIPBOARD), TerraFX.Interop.User32.WM_HSCROLLCLIPBOARD);
        public static readonly WindowMessage WM_ICONERASEBKGND = new WindowMessage(nameof(WM_ICONERASEBKGND), TerraFX.Interop.User32.WM_ICONERASEBKGND);
        public static readonly WindowMessage WM_IME_CHAR = new WindowMessage(nameof(WM_IME_CHAR), TerraFX.Interop.User32.WM_IME_CHAR);
        public static readonly WindowMessage WM_IME_COMPOSITION = new WindowMessage(nameof(WM_IME_COMPOSITION), TerraFX.Interop.User32.WM_IME_COMPOSITION);

        public static readonly WindowMessage WM_IME_COMPOSITIONFULL = new WindowMessage(
            nameof(WM_IME_COMPOSITIONFULL),
            TerraFX.Interop.User32.WM_IME_COMPOSITIONFULL);

        public static readonly WindowMessage WM_IME_CONTROL = new WindowMessage(nameof(WM_IME_CONTROL), TerraFX.Interop.User32.WM_IME_CONTROL);

        public static readonly WindowMessage WM_IME_ENDCOMPOSITION = new WindowMessage(
            nameof(WM_IME_ENDCOMPOSITION),
            TerraFX.Interop.User32.WM_IME_ENDCOMPOSITION);

        public static readonly WindowMessage WM_IME_KEYDOWN = new WindowMessage(nameof(WM_IME_KEYDOWN), TerraFX.Interop.User32.WM_IME_KEYDOWN);
        public static readonly WindowMessage WM_IME_KEYUP = new WindowMessage(nameof(WM_IME_KEYUP), TerraFX.Interop.User32.WM_IME_KEYUP);
        public static readonly WindowMessage WM_IME_NOTIFY = new WindowMessage(nameof(WM_IME_NOTIFY), TerraFX.Interop.User32.WM_IME_NOTIFY);
        public static readonly WindowMessage WM_IME_REQUEST = new WindowMessage(nameof(WM_IME_REQUEST), TerraFX.Interop.User32.WM_IME_REQUEST);
        public static readonly WindowMessage WM_IME_SELECT = new WindowMessage(nameof(WM_IME_SELECT), TerraFX.Interop.User32.WM_IME_SELECT);
        public static readonly WindowMessage WM_IME_SETCONTEXT = new WindowMessage(nameof(WM_IME_SETCONTEXT), TerraFX.Interop.User32.WM_IME_SETCONTEXT);

        public static readonly WindowMessage WM_IME_STARTCOMPOSITION = new WindowMessage(
            nameof(WM_IME_STARTCOMPOSITION),
            TerraFX.Interop.User32.WM_IME_STARTCOMPOSITION);

        public static readonly WindowMessage WM_INITDIALOG = new WindowMessage(nameof(WM_INITDIALOG), TerraFX.Interop.User32.WM_INITDIALOG);
        public static readonly WindowMessage WM_INITMENU = new WindowMessage(nameof(WM_INITMENU), TerraFX.Interop.User32.WM_INITMENU);
        public static readonly WindowMessage WM_INITMENUPOPUP = new WindowMessage(nameof(WM_INITMENUPOPUP), TerraFX.Interop.User32.WM_INITMENUPOPUP);
        public static readonly WindowMessage WM_INPUT = new WindowMessage(nameof(WM_INPUT), TerraFX.Interop.User32.WM_INPUT);

        public static readonly WindowMessage WM_INPUT_DEVICE_CHANGE = new WindowMessage(
            nameof(WM_INPUT_DEVICE_CHANGE),
            TerraFX.Interop.User32.WM_INPUT_DEVICE_CHANGE);

        public static readonly WindowMessage WM_INPUTLANGCHANGE = new WindowMessage(nameof(WM_INPUTLANGCHANGE), TerraFX.Interop.User32.WM_INPUTLANGCHANGE);

        public static readonly WindowMessage WM_INPUTLANGCHANGEREQUEST = new WindowMessage(
            nameof(WM_INPUTLANGCHANGEREQUEST),
            TerraFX.Interop.User32.WM_INPUTLANGCHANGEREQUEST);

        public static readonly WindowMessage WM_KEYDOWN = new WindowMessage(nameof(WM_KEYDOWN), TerraFX.Interop.User32.WM_KEYDOWN);
        public static readonly WindowMessage WM_KEYUP = new WindowMessage(nameof(WM_KEYUP), TerraFX.Interop.User32.WM_KEYUP);
        public static readonly WindowMessage WM_KILLFOCUS = new WindowMessage(nameof(WM_KILLFOCUS), TerraFX.Interop.User32.WM_KILLFOCUS);
        public static readonly WindowMessage WM_LBUTTONDBLCLK = new WindowMessage(nameof(WM_LBUTTONDBLCLK), TerraFX.Interop.User32.WM_LBUTTONDBLCLK);
        public static readonly WindowMessage WM_LBUTTONDOWN = new WindowMessage(nameof(WM_LBUTTONDOWN), TerraFX.Interop.User32.WM_LBUTTONDOWN);
        public static readonly WindowMessage WM_LBUTTONUP = new WindowMessage(nameof(WM_LBUTTONUP), TerraFX.Interop.User32.WM_LBUTTONUP);
        public static readonly WindowMessage WM_MBUTTONDBLCLK = new WindowMessage(nameof(WM_MBUTTONDBLCLK), TerraFX.Interop.User32.WM_MBUTTONDBLCLK);
        public static readonly WindowMessage WM_MBUTTONDOWN = new WindowMessage(nameof(WM_MBUTTONDOWN), TerraFX.Interop.User32.WM_MBUTTONDOWN);
        public static readonly WindowMessage WM_MBUTTONUP = new WindowMessage(nameof(WM_MBUTTONUP), TerraFX.Interop.User32.WM_MBUTTONUP);
        public static readonly WindowMessage WM_MDIACTIVATE = new WindowMessage(nameof(WM_MDIACTIVATE), TerraFX.Interop.User32.WM_MDIACTIVATE);
        public static readonly WindowMessage WM_MDICASCADE = new WindowMessage(nameof(WM_MDICASCADE), TerraFX.Interop.User32.WM_MDICASCADE);
        public static readonly WindowMessage WM_MDICREATE = new WindowMessage(nameof(WM_MDICREATE), TerraFX.Interop.User32.WM_MDICREATE);
        public static readonly WindowMessage WM_MDIDESTROY = new WindowMessage(nameof(WM_MDIDESTROY), TerraFX.Interop.User32.WM_MDIDESTROY);
        public static readonly WindowMessage WM_MDIGETACTIVE = new WindowMessage(nameof(WM_MDIGETACTIVE), TerraFX.Interop.User32.WM_MDIGETACTIVE);
        public static readonly WindowMessage WM_MDIICONARRANGE = new WindowMessage(nameof(WM_MDIICONARRANGE), TerraFX.Interop.User32.WM_MDIICONARRANGE);
        public static readonly WindowMessage WM_MDIMAXIMIZE = new WindowMessage(nameof(WM_MDIMAXIMIZE), TerraFX.Interop.User32.WM_MDIMAXIMIZE);
        public static readonly WindowMessage WM_MDINEXT = new WindowMessage(nameof(WM_MDINEXT), TerraFX.Interop.User32.WM_MDINEXT);
        public static readonly WindowMessage WM_MDIREFRESHMENU = new WindowMessage(nameof(WM_MDIREFRESHMENU), TerraFX.Interop.User32.WM_MDIREFRESHMENU);
        public static readonly WindowMessage WM_MDIRESTORE = new WindowMessage(nameof(WM_MDIRESTORE), TerraFX.Interop.User32.WM_MDIRESTORE);
        public static readonly WindowMessage WM_MDISETMENU = new WindowMessage(nameof(WM_MDISETMENU), TerraFX.Interop.User32.WM_MDISETMENU);
        public static readonly WindowMessage WM_MDITILE = new WindowMessage(nameof(WM_MDITILE), TerraFX.Interop.User32.WM_MDITILE);
        public static readonly WindowMessage WM_MEASUREITEM = new WindowMessage(nameof(WM_MEASUREITEM), TerraFX.Interop.User32.WM_MEASUREITEM);
        public static readonly WindowMessage WM_MENUCHAR = new WindowMessage(nameof(WM_MENUCHAR), TerraFX.Interop.User32.WM_MENUCHAR);
        public static readonly WindowMessage WM_MENUCOMMAND = new WindowMessage(nameof(WM_MENUCOMMAND), TerraFX.Interop.User32.WM_MENUCOMMAND);
        public static readonly WindowMessage WM_MENUDRAG = new WindowMessage(nameof(WM_MENUDRAG), TerraFX.Interop.User32.WM_MENUDRAG);
        public static readonly WindowMessage WM_MENUGETOBJECT = new WindowMessage(nameof(WM_MENUGETOBJECT), TerraFX.Interop.User32.WM_MENUGETOBJECT);
        public static readonly WindowMessage WM_MENURBUTTONUP = new WindowMessage(nameof(WM_MENURBUTTONUP), TerraFX.Interop.User32.WM_MENURBUTTONUP);
        public static readonly WindowMessage WM_MENUSELECT = new WindowMessage(nameof(WM_MENUSELECT), TerraFX.Interop.User32.WM_MENUSELECT);
        public static readonly WindowMessage WM_MOUSEACTIVATE = new WindowMessage(nameof(WM_MOUSEACTIVATE), TerraFX.Interop.User32.WM_MOUSEACTIVATE);
        public static readonly WindowMessage WM_MOUSEHOVER = new WindowMessage(nameof(WM_MOUSEHOVER), TerraFX.Interop.User32.WM_MOUSEHOVER);
        public static readonly WindowMessage WM_MOUSEHWHEEL = new WindowMessage(nameof(WM_MOUSEHWHEEL), TerraFX.Interop.User32.WM_MOUSEHWHEEL);
        public static readonly WindowMessage WM_MOUSELEAVE = new WindowMessage(nameof(WM_MOUSELEAVE), TerraFX.Interop.User32.WM_MOUSELEAVE);
        public static readonly WindowMessage WM_MOUSEMOVE = new WindowMessage(nameof(WM_MOUSEMOVE), TerraFX.Interop.User32.WM_MOUSEMOVE);
        public static readonly WindowMessage WM_MOUSEWHEEL = new WindowMessage(nameof(WM_MOUSEWHEEL), TerraFX.Interop.User32.WM_MOUSEWHEEL);
        public static readonly WindowMessage WM_MOVE = new WindowMessage(nameof(WM_MOVE), TerraFX.Interop.User32.WM_MOVE);
        public static readonly WindowMessage WM_MOVING = new WindowMessage(nameof(WM_MOVING), TerraFX.Interop.User32.WM_MOVING);
        public static readonly WindowMessage WM_NCACTIVATE = new WindowMessage(nameof(WM_NCACTIVATE), TerraFX.Interop.User32.WM_NCACTIVATE);
        public static readonly WindowMessage WM_NCCALCSIZE = new WindowMessage(nameof(WM_NCCALCSIZE), TerraFX.Interop.User32.WM_NCCALCSIZE);
        public static readonly WindowMessage WM_NCCREATE = new WindowMessage(nameof(WM_NCCREATE), TerraFX.Interop.User32.WM_NCCREATE);
        public static readonly WindowMessage WM_NCDESTROY = new WindowMessage(nameof(WM_NCDESTROY), TerraFX.Interop.User32.WM_NCDESTROY);
        public static readonly WindowMessage WM_NCHITTEST = new WindowMessage(nameof(WM_NCHITTEST), TerraFX.Interop.User32.WM_NCHITTEST);
        public static readonly WindowMessage WM_NCLBUTTONDBLCLK = new WindowMessage(nameof(WM_NCLBUTTONDBLCLK), TerraFX.Interop.User32.WM_NCLBUTTONDBLCLK);
        public static readonly WindowMessage WM_NCLBUTTONDOWN = new WindowMessage(nameof(WM_NCLBUTTONDOWN), TerraFX.Interop.User32.WM_NCLBUTTONDOWN);
        public static readonly WindowMessage WM_NCLBUTTONUP = new WindowMessage(nameof(WM_NCLBUTTONUP), TerraFX.Interop.User32.WM_NCLBUTTONUP);
        public static readonly WindowMessage WM_NCMBUTTONDBLCLK = new WindowMessage(nameof(WM_NCMBUTTONDBLCLK), TerraFX.Interop.User32.WM_NCMBUTTONDBLCLK);
        public static readonly WindowMessage WM_NCMBUTTONDOWN = new WindowMessage(nameof(WM_NCMBUTTONDOWN), TerraFX.Interop.User32.WM_NCMBUTTONDOWN);
        public static readonly WindowMessage WM_NCMBUTTONUP = new WindowMessage(nameof(WM_NCMBUTTONUP), TerraFX.Interop.User32.WM_NCMBUTTONUP);
        public static readonly WindowMessage WM_NCMOUSEHOVER = new WindowMessage(nameof(WM_NCMOUSEHOVER), TerraFX.Interop.User32.WM_NCMOUSEHOVER);
        public static readonly WindowMessage WM_NCMOUSELEAVE = new WindowMessage(nameof(WM_NCMOUSELEAVE), TerraFX.Interop.User32.WM_NCMOUSELEAVE);
        public static readonly WindowMessage WM_NCMOUSEMOVE = new WindowMessage(nameof(WM_NCMOUSEMOVE), TerraFX.Interop.User32.WM_NCMOUSEMOVE);
        public static readonly WindowMessage WM_NCPAINT = new WindowMessage(nameof(WM_NCPAINT), TerraFX.Interop.User32.WM_NCPAINT);
        public static readonly WindowMessage WM_NCPOINTERDOWN = new WindowMessage(nameof(WM_NCPOINTERDOWN), TerraFX.Interop.User32.WM_NCPOINTERDOWN);
        public static readonly WindowMessage WM_NCPOINTERUP = new WindowMessage(nameof(WM_NCPOINTERUP), TerraFX.Interop.User32.WM_NCPOINTERUP);
        public static readonly WindowMessage WM_NCPOINTERUPDATE = new WindowMessage(nameof(WM_NCPOINTERUPDATE), TerraFX.Interop.User32.WM_NCPOINTERUPDATE);
        public static readonly WindowMessage WM_NCRBUTTONDBLCLK = new WindowMessage(nameof(WM_NCRBUTTONDBLCLK), TerraFX.Interop.User32.WM_NCRBUTTONDBLCLK);
        public static readonly WindowMessage WM_NCRBUTTONDOWN = new WindowMessage(nameof(WM_NCRBUTTONDOWN), TerraFX.Interop.User32.WM_NCRBUTTONDOWN);
        public static readonly WindowMessage WM_NCRBUTTONUP = new WindowMessage(nameof(WM_NCRBUTTONUP), TerraFX.Interop.User32.WM_NCRBUTTONUP);
        public static readonly WindowMessage WM_NCXBUTTONDBLCLK = new WindowMessage(nameof(WM_NCXBUTTONDBLCLK), TerraFX.Interop.User32.WM_NCXBUTTONDBLCLK);
        public static readonly WindowMessage WM_NCXBUTTONDOWN = new WindowMessage(nameof(WM_NCXBUTTONDOWN), TerraFX.Interop.User32.WM_NCXBUTTONDOWN);
        public static readonly WindowMessage WM_NCXBUTTONUP = new WindowMessage(nameof(WM_NCXBUTTONUP), TerraFX.Interop.User32.WM_NCXBUTTONUP);
        public static readonly WindowMessage WM_NEXTDLGCTL = new WindowMessage(nameof(WM_NEXTDLGCTL), TerraFX.Interop.User32.WM_NEXTDLGCTL);
        public static readonly WindowMessage WM_NEXTMENU = new WindowMessage(nameof(WM_NEXTMENU), TerraFX.Interop.User32.WM_NEXTMENU);
        public static readonly WindowMessage WM_NOTIFY = new WindowMessage(nameof(WM_NOTIFY), TerraFX.Interop.User32.WM_NOTIFY);
        public static readonly WindowMessage WM_NOTIFYFORMAT = new WindowMessage(nameof(WM_NOTIFYFORMAT), TerraFX.Interop.User32.WM_NOTIFYFORMAT);
        public static readonly WindowMessage WM_NULL = new WindowMessage(nameof(WM_NULL), TerraFX.Interop.User32.WM_NULL);
        public static readonly WindowMessage WM_PAINT = new WindowMessage(nameof(WM_PAINT), TerraFX.Interop.User32.WM_PAINT);
        public static readonly WindowMessage WM_PAINTCLIPBOARD = new WindowMessage(nameof(WM_PAINTCLIPBOARD), TerraFX.Interop.User32.WM_PAINTCLIPBOARD);
        public static readonly WindowMessage WM_PAINTICON = new WindowMessage(nameof(WM_PAINTICON), TerraFX.Interop.User32.WM_PAINTICON);
        public static readonly WindowMessage WM_PALETTECHANGED = new WindowMessage(nameof(WM_PALETTECHANGED), TerraFX.Interop.User32.WM_PALETTECHANGED);

        public static readonly WindowMessage WM_PALETTEISCHANGING = new WindowMessage(
            nameof(WM_PALETTEISCHANGING),
            TerraFX.Interop.User32.WM_PALETTEISCHANGING);

        public static readonly WindowMessage WM_PARENTNOTIFY = new WindowMessage(nameof(WM_PARENTNOTIFY), TerraFX.Interop.User32.WM_PARENTNOTIFY);
        public static readonly WindowMessage WM_PASTE = new WindowMessage(nameof(WM_PASTE), TerraFX.Interop.User32.WM_PASTE);
        public static readonly WindowMessage WM_PENWINFIRST = new WindowMessage(nameof(WM_PENWINFIRST), TerraFX.Interop.User32.WM_PENWINFIRST);
        public static readonly WindowMessage WM_PENWINLAST = new WindowMessage(nameof(WM_PENWINLAST), TerraFX.Interop.User32.WM_PENWINLAST);
        public static readonly WindowMessage WM_POINTERACTIVATE = new WindowMessage(nameof(WM_POINTERACTIVATE), TerraFX.Interop.User32.WM_POINTERACTIVATE);

        public static readonly WindowMessage WM_POINTERCAPTURECHANGED = new WindowMessage(
            nameof(WM_POINTERCAPTURECHANGED),
            TerraFX.Interop.User32.WM_POINTERCAPTURECHANGED);

        public static readonly WindowMessage WM_POINTERDEVICECHANGE = new WindowMessage(
            nameof(WM_POINTERDEVICECHANGE),
            TerraFX.Interop.User32.WM_POINTERDEVICECHANGE);

        public static readonly WindowMessage WM_POINTERDEVICEINRANGE = new WindowMessage(
            nameof(WM_POINTERDEVICEINRANGE),
            TerraFX.Interop.User32.WM_POINTERDEVICEINRANGE);

        public static readonly WindowMessage WM_POINTERDEVICEOUTOFRANGE = new WindowMessage(
            nameof(WM_POINTERDEVICEOUTOFRANGE),
            TerraFX.Interop.User32.WM_POINTERDEVICEOUTOFRANGE);

        public static readonly WindowMessage WM_POINTERDOWN = new WindowMessage(nameof(WM_POINTERDOWN), TerraFX.Interop.User32.WM_POINTERDOWN);
        public static readonly WindowMessage WM_POINTERENTER = new WindowMessage(nameof(WM_POINTERENTER), TerraFX.Interop.User32.WM_POINTERENTER);
        public static readonly WindowMessage WM_POINTERHWHEEL = new WindowMessage(nameof(WM_POINTERHWHEEL), TerraFX.Interop.User32.WM_POINTERHWHEEL);
        public static readonly WindowMessage WM_POINTERLEAVE = new WindowMessage(nameof(WM_POINTERLEAVE), TerraFX.Interop.User32.WM_POINTERLEAVE);

        public static readonly WindowMessage WM_POINTERROUTEDAWAY = new WindowMessage(
            nameof(WM_POINTERROUTEDAWAY),
            TerraFX.Interop.User32.WM_POINTERROUTEDAWAY);

        public static readonly WindowMessage WM_POINTERROUTEDRELEASED = new WindowMessage(
            nameof(WM_POINTERROUTEDRELEASED),
            TerraFX.Interop.User32.WM_POINTERROUTEDRELEASED);

        public static readonly WindowMessage WM_POINTERROUTEDTO = new WindowMessage(nameof(WM_POINTERROUTEDTO), TerraFX.Interop.User32.WM_POINTERROUTEDTO);
        public static readonly WindowMessage WM_POINTERUP = new WindowMessage(nameof(WM_POINTERUP), TerraFX.Interop.User32.WM_POINTERUP);
        public static readonly WindowMessage WM_POINTERUPDATE = new WindowMessage(nameof(WM_POINTERUPDATE), TerraFX.Interop.User32.WM_POINTERUPDATE);
        public static readonly WindowMessage WM_POINTERWHEEL = new WindowMessage(nameof(WM_POINTERWHEEL), TerraFX.Interop.User32.WM_POINTERWHEEL);
        public static readonly WindowMessage WM_POWER = new WindowMessage(nameof(WM_POWER), TerraFX.Interop.User32.WM_POWER);
        public static readonly WindowMessage WM_POWERBROADCAST = new WindowMessage(nameof(WM_POWERBROADCAST), TerraFX.Interop.User32.WM_POWERBROADCAST);
        public static readonly WindowMessage WM_PRINT = new WindowMessage(nameof(WM_PRINT), TerraFX.Interop.User32.WM_PRINT);
        public static readonly WindowMessage WM_PRINTCLIENT = new WindowMessage(nameof(WM_PRINTCLIENT), TerraFX.Interop.User32.WM_PRINTCLIENT);
        public static readonly WindowMessage WM_QUERYDRAGICON = new WindowMessage(nameof(WM_QUERYDRAGICON), TerraFX.Interop.User32.WM_QUERYDRAGICON);
        public static readonly WindowMessage WM_QUERYENDSESSION = new WindowMessage(nameof(WM_QUERYENDSESSION), TerraFX.Interop.User32.WM_QUERYENDSESSION);
        public static readonly WindowMessage WM_QUERYNEWPALETTE = new WindowMessage(nameof(WM_QUERYNEWPALETTE), TerraFX.Interop.User32.WM_QUERYNEWPALETTE);
        public static readonly WindowMessage WM_QUERYOPEN = new WindowMessage(nameof(WM_QUERYOPEN), TerraFX.Interop.User32.WM_QUERYOPEN);
        public static readonly WindowMessage WM_QUERYUISTATE = new WindowMessage(nameof(WM_QUERYUISTATE), TerraFX.Interop.User32.WM_QUERYUISTATE);
        public static readonly WindowMessage WM_QUEUESYNC = new WindowMessage(nameof(WM_QUEUESYNC), TerraFX.Interop.User32.WM_QUEUESYNC);
        public static readonly WindowMessage WM_QUIT = new WindowMessage(nameof(WM_QUIT), TerraFX.Interop.User32.WM_QUIT);
        public static readonly WindowMessage WM_RBUTTONDBLCLK = new WindowMessage(nameof(WM_RBUTTONDBLCLK), TerraFX.Interop.User32.WM_RBUTTONDBLCLK);
        public static readonly WindowMessage WM_RBUTTONDOWN = new WindowMessage(nameof(WM_RBUTTONDOWN), TerraFX.Interop.User32.WM_RBUTTONDOWN);
        public static readonly WindowMessage WM_RBUTTONUP = new WindowMessage(nameof(WM_RBUTTONUP), TerraFX.Interop.User32.WM_RBUTTONUP);
        public static readonly WindowMessage WM_RENDERALLFORMATS = new WindowMessage(nameof(WM_RENDERALLFORMATS), TerraFX.Interop.User32.WM_RENDERALLFORMATS);
        public static readonly WindowMessage WM_RENDERFORMAT = new WindowMessage(nameof(WM_RENDERFORMAT), TerraFX.Interop.User32.WM_RENDERFORMAT);
        public static readonly WindowMessage WM_SETCURSOR = new WindowMessage(nameof(WM_SETCURSOR), TerraFX.Interop.User32.WM_SETCURSOR);
        public static readonly WindowMessage WM_SETFOCUS = new WindowMessage(nameof(WM_SETFOCUS), TerraFX.Interop.User32.WM_SETFOCUS);
        public static readonly WindowMessage WM_SETFONT = new WindowMessage(nameof(WM_SETFONT), TerraFX.Interop.User32.WM_SETFONT);
        public static readonly WindowMessage WM_SETHOTKEY = new WindowMessage(nameof(WM_SETHOTKEY), TerraFX.Interop.User32.WM_SETHOTKEY);
        public static readonly WindowMessage WM_SETICON = new WindowMessage(nameof(WM_SETICON), TerraFX.Interop.User32.WM_SETICON);
        public static readonly WindowMessage WM_SETREDRAW = new WindowMessage(nameof(WM_SETREDRAW), TerraFX.Interop.User32.WM_SETREDRAW);
        public static readonly WindowMessage WM_SETTEXT = new WindowMessage(nameof(WM_SETTEXT), TerraFX.Interop.User32.WM_SETTEXT);
        public static readonly WindowMessage WM_SETTINGCHANGE = new WindowMessage(nameof(WM_SETTINGCHANGE), TerraFX.Interop.User32.WM_SETTINGCHANGE);
        public static readonly WindowMessage WM_SHOWWINDOW = new WindowMessage(nameof(WM_SHOWWINDOW), TerraFX.Interop.User32.WM_SHOWWINDOW);
        public static readonly WindowMessage WM_SIZE = new WindowMessage(nameof(WM_SIZE), TerraFX.Interop.User32.WM_SIZE);
        public static readonly WindowMessage WM_SIZECLIPBOARD = new WindowMessage(nameof(WM_SIZECLIPBOARD), TerraFX.Interop.User32.WM_SIZECLIPBOARD);
        public static readonly WindowMessage WM_SIZING = new WindowMessage(nameof(WM_SIZING), TerraFX.Interop.User32.WM_SIZING);
        public static readonly WindowMessage WM_SPOOLERSTATUS = new WindowMessage(nameof(WM_SPOOLERSTATUS), TerraFX.Interop.User32.WM_SPOOLERSTATUS);
        public static readonly WindowMessage WM_STYLECHANGED = new WindowMessage(nameof(WM_STYLECHANGED), TerraFX.Interop.User32.WM_STYLECHANGED);
        public static readonly WindowMessage WM_STYLECHANGING = new WindowMessage(nameof(WM_STYLECHANGING), TerraFX.Interop.User32.WM_STYLECHANGING);
        public static readonly WindowMessage WM_SYNCPAINT = new WindowMessage(nameof(WM_SYNCPAINT), TerraFX.Interop.User32.WM_SYNCPAINT);
        public static readonly WindowMessage WM_SYSCHAR = new WindowMessage(nameof(WM_SYSCHAR), TerraFX.Interop.User32.WM_SYSCHAR);
        public static readonly WindowMessage WM_SYSCOLORCHANGE = new WindowMessage(nameof(WM_SYSCOLORCHANGE), TerraFX.Interop.User32.WM_SYSCOLORCHANGE);
        public static readonly WindowMessage WM_SYSCOMMAND = new WindowMessage(nameof(WM_SYSCOMMAND), TerraFX.Interop.User32.WM_SYSCOMMAND);
        public static readonly WindowMessage WM_SYSDEADCHAR = new WindowMessage(nameof(WM_SYSDEADCHAR), TerraFX.Interop.User32.WM_SYSDEADCHAR);
        public static readonly WindowMessage WM_SYSKEYDOWN = new WindowMessage(nameof(WM_SYSKEYDOWN), TerraFX.Interop.User32.WM_SYSKEYDOWN);
        public static readonly WindowMessage WM_SYSKEYUP = new WindowMessage(nameof(WM_SYSKEYUP), TerraFX.Interop.User32.WM_SYSKEYUP);
        public static readonly WindowMessage WM_TABLET_FIRST = new WindowMessage(nameof(WM_TABLET_FIRST), TerraFX.Interop.User32.WM_TABLET_FIRST);
        public static readonly WindowMessage WM_TABLET_LAST = new WindowMessage(nameof(WM_TABLET_LAST), TerraFX.Interop.User32.WM_TABLET_LAST);
        public static readonly WindowMessage WM_TCARD = new WindowMessage(nameof(WM_TCARD), TerraFX.Interop.User32.WM_TCARD);
        public static readonly WindowMessage WM_THEMECHANGED = new WindowMessage(nameof(WM_THEMECHANGED), TerraFX.Interop.User32.WM_THEMECHANGED);
        public static readonly WindowMessage WM_TIMECHANGE = new WindowMessage(nameof(WM_TIMECHANGE), TerraFX.Interop.User32.WM_TIMECHANGE);
        public static readonly WindowMessage WM_TIMER = new WindowMessage(nameof(WM_TIMER), TerraFX.Interop.User32.WM_TIMER);
        public static readonly WindowMessage WM_TOUCH = new WindowMessage(nameof(WM_TOUCH), TerraFX.Interop.User32.WM_TOUCH);
        public static readonly WindowMessage WM_TOUCHHITTESTING = new WindowMessage(nameof(WM_TOUCHHITTESTING), TerraFX.Interop.User32.WM_TOUCHHITTESTING);
        public static readonly WindowMessage WM_UNDO = new WindowMessage(nameof(WM_UNDO), TerraFX.Interop.User32.WM_UNDO);
        public static readonly WindowMessage WM_UNICHAR = new WindowMessage(nameof(WM_UNICHAR), TerraFX.Interop.User32.WM_UNICHAR);
        public static readonly WindowMessage WM_UNINITMENUPOPUP = new WindowMessage(nameof(WM_UNINITMENUPOPUP), TerraFX.Interop.User32.WM_UNINITMENUPOPUP);
        public static readonly WindowMessage WM_UPDATEUISTATE = new WindowMessage(nameof(WM_UPDATEUISTATE), TerraFX.Interop.User32.WM_UPDATEUISTATE);
        public static readonly WindowMessage WM_USER = new WindowMessage(nameof(WM_USER), TerraFX.Interop.User32.WM_USER);
        public static readonly WindowMessage WM_USERCHANGED = new WindowMessage(nameof(WM_USERCHANGED), TerraFX.Interop.User32.WM_USERCHANGED);
        public static readonly WindowMessage WM_VKEYTOITEM = new WindowMessage(nameof(WM_VKEYTOITEM), TerraFX.Interop.User32.WM_VKEYTOITEM);
        public static readonly WindowMessage WM_VSCROLL = new WindowMessage(nameof(WM_VSCROLL), TerraFX.Interop.User32.WM_VSCROLL);
        public static readonly WindowMessage WM_VSCROLLCLIPBOARD = new WindowMessage(nameof(WM_VSCROLLCLIPBOARD), TerraFX.Interop.User32.WM_VSCROLLCLIPBOARD);
        public static readonly WindowMessage WM_WINDOWPOSCHANGED = new WindowMessage(nameof(WM_WINDOWPOSCHANGED), TerraFX.Interop.User32.WM_WINDOWPOSCHANGED);

        public static readonly WindowMessage WM_WINDOWPOSCHANGING = new WindowMessage(
            nameof(WM_WINDOWPOSCHANGING),
            TerraFX.Interop.User32.WM_WINDOWPOSCHANGING);

        public static readonly WindowMessage WM_WTSSESSION_CHANGE = new WindowMessage(
            nameof(WM_WTSSESSION_CHANGE),
            TerraFX.Interop.User32.WM_WTSSESSION_CHANGE);

        public static readonly WindowMessage WM_XBUTTONDBLCLK = new WindowMessage(nameof(WM_XBUTTONDBLCLK), TerraFX.Interop.User32.WM_XBUTTONDBLCLK);
        public static readonly WindowMessage WM_XBUTTONDOWN = new WindowMessage(nameof(WM_XBUTTONDOWN), TerraFX.Interop.User32.WM_XBUTTONDOWN);
        public static readonly WindowMessage WM_XBUTTONUP = new WindowMessage(nameof(WM_XBUTTONUP), TerraFX.Interop.User32.WM_XBUTTONUP);

        private static readonly WindowsMessageCollection _windowsMessageByValue =
            new WindowsMessageCollection
            {
                MN_GETHMENU,
                WM_ACTIVATE,
                WM_ACTIVATEAPP,
                WM_AFXFIRST,
                WM_AFXLAST,
                WM_APP,
                WM_APPCOMMAND,
                WM_ASKCBFORMATNAME,
                WM_CANCELJOURNAL,
                WM_CANCELMODE,
                WM_CAPTURECHANGED,
                WM_CHANGECBCHAIN,
                WM_CHANGEUISTATE,
                WM_CHAR,
                WM_CHARTOITEM,
                WM_CHILDACTIVATE,
                WM_CLEAR,
                WM_CLIPBOARDUPDATE,
                WM_CLOSE,
                WM_COMMAND,
                WM_COMMNOTIFY,
                WM_COMPACTING,
                WM_COMPAREITEM,
                WM_CONTEXTMENU,
                WM_COPY,
                WM_COPYDATA,
                WM_CREATE,
                WM_CTLCOLORBTN,
                WM_CTLCOLORDLG,
                WM_CTLCOLOREDIT,
                WM_CTLCOLORLISTBOX,
                WM_CTLCOLORMSGBOX,
                WM_CTLCOLORSCROLLBAR,
                WM_CTLCOLORSTATIC,
                WM_CUT,
                WM_DEADCHAR,
                WM_DELETEITEM,
                WM_DESTROY,
                WM_DESTROYCLIPBOARD,
                WM_DEVICECHANGE,
                WM_DEVMODECHANGE,
                WM_DISPLAYCHANGE,
                WM_DPICHANGED,
                WM_DPICHANGED_AFTERPARENT,
                WM_DPICHANGED_BEFOREPARENT,
                WM_DRAWCLIPBOARD,
                WM_DRAWITEM,
                WM_DROPFILES,
                WM_DWMCOLORIZATIONCOLORCHANGED,
                WM_DWMCOMPOSITIONCHANGED,
                WM_DWMNCRENDERINGCHANGED,
                WM_DWMSENDICONICLIVEPREVIEWBITMAP,
                WM_DWMSENDICONICTHUMBNAIL,
                WM_DWMWINDOWMAXIMIZEDCHANGE,
                WM_ENABLE,
                WM_ENDSESSION,
                WM_ENTERIDLE,
                WM_ENTERMENULOOP,
                WM_ENTERSIZEMOVE,
                WM_ERASEBKGND,
                WM_EXITMENULOOP,
                WM_EXITSIZEMOVE,
                WM_FONTCHANGE,
                WM_GESTURE,
                WM_GESTURENOTIFY,
                WM_GETDLGCODE,
                WM_GETDPISCALEDSIZE,
                WM_GETFONT,
                WM_GETHOTKEY,
                WM_GETICON,
                WM_GETMINMAXINFO,
                WM_GETOBJECT,
                WM_GETTEXT,
                WM_GETTEXTLENGTH,
                WM_GETTITLEBARINFOEX,
                WM_HANDHELDFIRST,
                WM_HANDHELDLAST,
                WM_HELP,
                WM_HOTKEY,
                WM_HSCROLL,
                WM_HSCROLLCLIPBOARD,
                WM_ICONERASEBKGND,
                WM_IME_CHAR,
                WM_IME_COMPOSITION,
                WM_IME_COMPOSITIONFULL,
                WM_IME_CONTROL,
                WM_IME_ENDCOMPOSITION,
                WM_IME_KEYDOWN,
                WM_IME_KEYUP,
                WM_IME_NOTIFY,
                WM_IME_REQUEST,
                WM_IME_SELECT,
                WM_IME_SETCONTEXT,
                WM_IME_STARTCOMPOSITION,
                WM_INITDIALOG,
                WM_INITMENU,
                WM_INITMENUPOPUP,
                WM_INPUT,
                WM_INPUT_DEVICE_CHANGE,
                WM_INPUTLANGCHANGE,
                WM_INPUTLANGCHANGEREQUEST,
                WM_KEYDOWN,
                WM_KEYUP,
                WM_KILLFOCUS,
                WM_LBUTTONDBLCLK,
                WM_LBUTTONDOWN,
                WM_LBUTTONUP,
                WM_MBUTTONDBLCLK,
                WM_MBUTTONDOWN,
                WM_MBUTTONUP,
                WM_MDIACTIVATE,
                WM_MDICASCADE,
                WM_MDICREATE,
                WM_MDIDESTROY,
                WM_MDIGETACTIVE,
                WM_MDIICONARRANGE,
                WM_MDIMAXIMIZE,
                WM_MDINEXT,
                WM_MDIREFRESHMENU,
                WM_MDIRESTORE,
                WM_MDISETMENU,
                WM_MDITILE,
                WM_MEASUREITEM,
                WM_MENUCHAR,
                WM_MENUCOMMAND,
                WM_MENUDRAG,
                WM_MENUGETOBJECT,
                WM_MENURBUTTONUP,
                WM_MENUSELECT,
                WM_MOUSEACTIVATE,
                WM_MOUSEHOVER,
                WM_MOUSEHWHEEL,
                WM_MOUSELEAVE,
                WM_MOUSEMOVE,
                WM_MOUSEWHEEL,
                WM_MOVE,
                WM_MOVING,
                WM_NCACTIVATE,
                WM_NCCALCSIZE,
                WM_NCCREATE,
                WM_NCDESTROY,
                WM_NCHITTEST,
                WM_NCLBUTTONDBLCLK,
                WM_NCLBUTTONDOWN,
                WM_NCLBUTTONUP,
                WM_NCMBUTTONDBLCLK,
                WM_NCMBUTTONDOWN,
                WM_NCMBUTTONUP,
                WM_NCMOUSEHOVER,
                WM_NCMOUSELEAVE,
                WM_NCMOUSEMOVE,
                WM_NCPAINT,
                WM_NCPOINTERDOWN,
                WM_NCPOINTERUP,
                WM_NCPOINTERUPDATE,
                WM_NCRBUTTONDBLCLK,
                WM_NCRBUTTONDOWN,
                WM_NCRBUTTONUP,
                WM_NCXBUTTONDBLCLK,
                WM_NCXBUTTONDOWN,
                WM_NCXBUTTONUP,
                WM_NEXTDLGCTL,
                WM_NEXTMENU,
                WM_NOTIFY,
                WM_NOTIFYFORMAT,
                WM_NULL,
                WM_PAINT,
                WM_PAINTCLIPBOARD,
                WM_PAINTICON,
                WM_PALETTECHANGED,
                WM_PALETTEISCHANGING,
                WM_PARENTNOTIFY,
                WM_PASTE,
                WM_PENWINFIRST,
                WM_PENWINLAST,
                WM_POINTERACTIVATE,
                WM_POINTERCAPTURECHANGED,
                WM_POINTERDEVICECHANGE,
                WM_POINTERDEVICEINRANGE,
                WM_POINTERDEVICEOUTOFRANGE,
                WM_POINTERDOWN,
                WM_POINTERENTER,
                WM_POINTERHWHEEL,
                WM_POINTERLEAVE,
                WM_POINTERROUTEDAWAY,
                WM_POINTERROUTEDRELEASED,
                WM_POINTERROUTEDTO,
                WM_POINTERUP,
                WM_POINTERUPDATE,
                WM_POINTERWHEEL,
                WM_POWER,
                WM_POWERBROADCAST,
                WM_PRINT,
                WM_PRINTCLIENT,
                WM_QUERYDRAGICON,
                WM_QUERYENDSESSION,
                WM_QUERYNEWPALETTE,
                WM_QUERYOPEN,
                WM_QUERYUISTATE,
                WM_QUEUESYNC,
                WM_QUIT,
                WM_RBUTTONDBLCLK,
                WM_RBUTTONDOWN,
                WM_RBUTTONUP,
                WM_RENDERALLFORMATS,
                WM_RENDERFORMAT,
                WM_SETCURSOR,
                WM_SETFOCUS,
                WM_SETFONT,
                WM_SETHOTKEY,
                WM_SETICON,
                WM_SETREDRAW,
                WM_SETTEXT,
                WM_SETTINGCHANGE,
                WM_SHOWWINDOW,
                WM_SIZE,
                WM_SIZECLIPBOARD,
                WM_SIZING,
                WM_SPOOLERSTATUS,
                WM_STYLECHANGED,
                WM_STYLECHANGING,
                WM_SYNCPAINT,
                WM_SYSCHAR,
                WM_SYSCOLORCHANGE,
                WM_SYSCOMMAND,
                WM_SYSDEADCHAR,
                WM_SYSKEYDOWN,
                WM_SYSKEYUP,
                WM_TABLET_FIRST,
                WM_TABLET_LAST,
                WM_TCARD,
                WM_THEMECHANGED,
                WM_TIMECHANGE,
                WM_TIMER,
                WM_TOUCH,
                WM_TOUCHHITTESTING,
                WM_UNDO,
                WM_UNICHAR,
                WM_UNINITMENUPOPUP,
                WM_UPDATEUISTATE,
                WM_USER,
                WM_USERCHANGED,
                WM_VKEYTOITEM,
                WM_VSCROLL,
                WM_VSCROLLCLIPBOARD,
                WM_WINDOWPOSCHANGED,
                WM_WINDOWPOSCHANGING,
                WM_WTSSESSION_CHANGE,
                WM_XBUTTONDBLCLK,
                WM_XBUTTONDOWN,
                WM_XBUTTONUP
            };

        /// <summary>Initializes a new instance of the <see cref="WindowMessage" /> type.</summary>
        /// <param name="name">The string representation of the constant name.</param>
        /// <param name="value">The constant value.</param>
        public WindowMessage(string name, int value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>Gets the string representation of the constant name.</summary>
        public string Name { get; }

        /// <summary>Gets the constant value.</summary>
        public int Value { get; }

        /// <summary>Casts the <see cref="WindowMessage" /> instance to its constant value.</summary>
        public static implicit operator int(WindowMessage value)
        {
            return value.Value;
        }

        /// <summary>Attempts to map a constant value to a known window message.</summary>
        /// <param name="value">A constant value.</param>
        /// <returns>Returns the known window message, if one exists; otherwise, returns null.</returns>
        public static WindowMessage? MapKnown(int value)
        {
            _windowsMessageByValue.TryGetValue(value, out WindowMessage? windowsMessage);

            return windowsMessage;
        }

        /// <summary>A collection of window messages keyed by value.</summary>
        private class WindowsMessageCollection : KeyedCollection<int, WindowMessage>
        {
            /// <inheritdoc />
            protected override int GetKeyForItem(WindowMessage item)
            {
                return item.Value;
            }
        }
    }
}