# Rendering

[Direct2D](https://docs.microsoft.com/en-us/windows/win32/direct2d/direct2d-portal) is a Windows library that simplifies 2D rendering using a GPU. The engine uses Direct2D almost exclusively in order to render visuals.

[Direct3D](https://docs.microsoft.com/en-us/windows/win32/direct3d)--specifically Direct3D 11--is only used to provide a render target for Direct2D. The engine does not currently support rendering 3D visuals.

## Vertical sync

The engine supports an optional vertical sync capability. Turning on VSync will cap framerates at the target monitor's refresh rate, even if frametimes would allow for higher frames per second.
