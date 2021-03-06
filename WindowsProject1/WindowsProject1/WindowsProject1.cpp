// WindowsProject1.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "WindowsProject1.h"
#include <vector>

using namespace std;

#define MAX_LOADSTRING 100

// Global Variables:
HINSTANCE hInst;                                // current instance
WCHAR szTitle[MAX_LOADSTRING];                  // The title bar text
WCHAR szWindowClass[MAX_LOADSTRING];            // the main window class name
int state = 0;									// The state of the traffic lights
vector<int> south;								// List of cars from the south
vector<int> west;								// List of cars from the west
int tick = 0;									// Tick for timer
int pw = 0;										// Probability of car spawn from west
int ps = 0;										// Probability of car spawn for south
int ran = 0;									// Probability definer 
BOOL relative = false;							// Check for minus


// Forward declarations of functions included in this code module:
ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK	Probs(HWND, UINT, WPARAM, LPARAM);

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPWSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);

    // TODO: Place code here.

    // Initialize global strings
    LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
    LoadStringW(hInstance, IDC_WINDOWSPROJECT1, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);

    // Perform application initialization:
    if (!InitInstance (hInstance, nCmdShow))
    {
        return FALSE;
    }

    HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_WINDOWSPROJECT1));

    MSG msg;

    // Main message loop:
    while (GetMessage(&msg, nullptr, 0, 0))
    {
        if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
        {
            TranslateMessage(&msg);
            DispatchMessage(&msg);
        }
    }

    return (int) msg.wParam;
}



//
//  FUNCTION: MyRegisterClass()
//
//  PURPOSE: Registers the window class.
//
ATOM MyRegisterClass(HINSTANCE hInstance)
{
    WNDCLASSEXW wcex;

    wcex.cbSize = sizeof(WNDCLASSEX);

    wcex.style          = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc    = WndProc;
    wcex.cbClsExtra     = 0;
    wcex.cbWndExtra     = 0;
    wcex.hInstance      = hInstance;
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_WINDOWSPROJECT1));
    wcex.hCursor        = LoadCursor(nullptr, IDC_CROSS);
    wcex.hbrBackground  = (HBRUSH)CreateSolidBrush(RGB(50, 150, 25));
    wcex.lpszMenuName   = MAKEINTRESOURCEW(IDC_WINDOWSPROJECT1);
    wcex.lpszClassName  = szWindowClass;
    wcex.hIconSm        = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

    return RegisterClassExW(&wcex);
}

//
//   FUNCTION: InitInstance(HINSTANCE, int)
//
//   PURPOSE: Saves instance handle and creates main window
//
//   COMMENTS:
//
//        In this function, we save the instance handle in a global variable and
//        create and display the main program window.
//
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
   hInst = hInstance; // Store instance handle in our global variable

   HWND hWnd = CreateWindowW(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
      CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, nullptr, nullptr, hInstance, nullptr);

   if (!hWnd)
   {
      return FALSE;
   }

   ShowWindow(hWnd, nCmdShow);
   UpdateWindow(hWnd);

   return TRUE;
}



// FUNCTION: AddCarS(HDC, int, int) 
//
// PURPOSE: Add car from the South
//
bool AddCarS(HDC hdc, int x, int y) {

	HDC hdcMemDC = NULL;
	HBITMAP hCar = LoadBitmap(hInst, MAKEINTRESOURCE(IDB_BITMAP1));

	// Create a compatible DC which is used in a BitBlt from the window DC
	hdcMemDC = CreateCompatibleDC(hdc);

	// Select the compatible bitmap into the compatible memory DC.
	SelectObject(hdcMemDC, hCar);

	// Bit block transfer into our compatible memory DC.
	StretchBlt(hdc, x, y, 50, 108, hdcMemDC, 0, 0, 50, 108, SRCCOPY);
	

	// Cleanup
	DeleteObject(hCar);
	DeleteObject(hdcMemDC);

	return true;

}

// FUNCTION: AddCarW(HDC, int, int) 
//
// PURPOSE: Add car from the West
//
bool AddCarW(HDC hdc, int x, int y) {

	HDC hdcMemDC = NULL;
	HBITMAP hCar = LoadBitmap(hInst, MAKEINTRESOURCE(IDB_BITMAP2));

	// Create a compatible DC which is used in a BitBlt from the window DC
	hdcMemDC = CreateCompatibleDC(hdc);

	// Select the compatible bitmap into the compatible memory DC.
	SelectObject(hdcMemDC, hCar);

	// Bit block transfer into our compatible memory DC.
	StretchBlt(hdc, x, y, 108, 50, hdcMemDC, 0, 0, 108, 50, SRCCOPY);


	// Cleanup
	DeleteObject(hCar);
	DeleteObject(hdcMemDC);

	return true;

}

//
//  FUNCTION: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  PURPOSE:  Processes messages for the main window.
//
//  WM_COMMAND  - process the application menu
//  WM_PAINT    - Paint the main window
//  WM_DESTROY  - post a quit message and return
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	// Responsive
	RECT screensize;
	GetClientRect(hWnd, &screensize);
	int X = screensize.right;
	int Y = screensize.bottom;
	
    switch (message)
    {
	case WM_CREATE:
		SetTimer(hWnd, 1, 10, NULL);
		break;
    case WM_COMMAND:
        {
            int wmId = LOWORD(wParam);
            // Parse the menu selections:
            switch (wmId)
            {
			case ID_MINMENY_PROBS:
				DialogBox(hInst, MAKEINTRESOURCE(IDD_DIALOG1), hWnd, Probs);
				break;
            case IDM_ABOUT:
                DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
                break;
            case IDM_EXIT:
                DestroyWindow(hWnd);
                break;
            default:
                return DefWindowProc(hWnd, message, wParam, lParam);
            }
        }
        break;
    case WM_PAINT:
        {
            PAINTSTRUCT ps;
            HDC hdc = BeginPaint(hWnd, &ps);

			// GDI logic for creating a brush
			HBRUSH hBrush = CreateSolidBrush(RGB(0, 0, 0));
			HGDIOBJ hOrg = SelectObject(hdc, hBrush);

			// Creating the road
			Rectangle(hdc, 0, (Y / 2 - 50), X, (Y / 2 + 50));
			Rectangle(hdc, (X / 2 - 50), 0, (X / 2 + 50), Y);

			// Create the lights
			Trafikklys* lys0 = new Trafikklys(hdc, (X / 2 - 80), (Y / 2 - 130), state);
			Trafikklys* lys1 = new Trafikklys(hdc, (X / 2 + 80), (Y / 2 + 130), ((state + 2) % 4));

			// Adding cars
			vector<int>::iterator i;
			for (i = west.begin(); i != west.end(); i++) {
				AddCarW(hdc, *i, Y / 2 - 25);
			}
			for (i = south.begin(); i != south.end(); i++) {
				AddCarS(hdc, X / 2 - 25, *i);
			}

			// Adding the probs in a corner

			// Cleanup, set back original brush 
			SelectObject(hdc, hOrg);
			DeleteObject(hBrush);
            EndPaint(hWnd, &ps);
        }
        break;
    case WM_DESTROY:
        PostQuitMessage(0);
        break;
	case WM_KEYDOWN:
		switch (wParam) {
		case VK_UP:
			ps = (ps + 10) % 100;
			break;
		case VK_DOWN:
			ps = (ps - 10) % 100;
			break;
		case VK_LEFT:
			pw = (pw - 10) % 100;
			break;
		case VK_RIGHT:
			pw = (pw + 10) % 100;
			break;
		}
		break;
	case WM_LBUTTONDOWN:
		break;
	case WM_TIMER:
		// Increase tick
		tick++;

		// Move the cars
		for (int j = 0; j < 4; j++) {
			for (int i = 0; i < west.size(); i++) {
				if (west[i] == (X / 2 - 158)) {
					if (state == 2) {
						west[i]++;
					}

				}
				else if (i < west.size() - 1 && west[i] < (west[i + 1] - 108)) {
					west[i]++;
				}
				else if (west[i] == west.back()) {
					if (west[i] == X - 50) {
						west.pop_back();
					}
					else {
						west[i]++;
					}
				}
			}

			for (int k = 0; k < south.size(); k++) {
				if (south[k] == (Y / 2 + 50)) {
					if (state == 0) {
						south[k]--;
					}

				}
				else if (k < south.size() - 1 && south[k] > (south[k + 1] + 108)) {
					south[k]--;
				}
				else if (south[k] == south.back()) {
					if (south[k] == 50) {
						south.pop_back();
					}
					else {
						south[k]--;
					}
				}
			}
		}

		if (tick % 100 == 0) {
			// Add cars per probability
			ran = rand() % 100;
			if (ran <= pw) {
				if (west.empty()) {
					west.push_back(0);
				}
				else if (west[0] > 110) {
					west.insert(west.begin(), 0);
				}
			}
			ran = rand() % 100;
			if (ran <= ps) {
				if (south.empty()) {
					south.push_back(Y - 108);
				}

				if (south[0] < Y - 210) {
					south.insert(south.begin(), Y - 108);
				}
			}

			// Change state per second
			state = (state + 1) % 4;
		}
		InvalidateRect(hWnd, NULL, FALSE);
		break;
	case WM_RBUTTONDOWN:
		break;
    default:
        return DefWindowProc(hWnd, message, wParam, lParam);
    }
    return 0;
}

// Message handler for about box.
INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
    UNREFERENCED_PARAMETER(lParam);
    switch (message)
    {
    case WM_INITDIALOG:
        return (INT_PTR)TRUE;

    case WM_COMMAND:
        if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
        {
            EndDialog(hDlg, LOWORD(wParam));
            return (INT_PTR)TRUE;
        }
        break;
    }
    return (INT_PTR)FALSE;
}

// Message handler for probability box.
INT_PTR CALLBACK Probs(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	UNREFERENCED_PARAMETER(lParam);
	static BOOL error = true;

	switch (message)
	{

	case WM_INITDIALOG:
		return (INT_PTR)TRUE;

	case WM_COMMAND:
		switch (LOWORD(wParam)) {
		case IDOK:
			// Gets the values from the editboxes
			ps = ::GetDlgItemInt(hDlg, IDC_EDIT1, &error, relative);
			pw = ::GetDlgItemInt(hDlg, IDC_EDIT2, &error, relative);
			
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;

		case IDCANCEL:

			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		}
		break;
	}
	return (INT_PTR)FALSE;
}