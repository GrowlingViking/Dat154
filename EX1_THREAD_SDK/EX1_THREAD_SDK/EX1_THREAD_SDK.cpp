// EX1_THREAD_SDK.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "process.h"
#include "math.h"
#include "EX1_THREAD_SDK.h"

#define MAX_LOADSTRING 100

// Global Variables:
HINSTANCE hInst;								// current instance
TCHAR szTitle[MAX_LOADSTRING];					// The title bar text
TCHAR szWindowClass[MAX_LOADSTRING];			// the main window class name


// THREAD STUFF
const int nThreads = 2;
const int priority = THREAD_PRIORITY_NORMAL;
const bool fSuspendThreadInLoop=true;
const long nPrimes = 15000000;
// THREAD STUFF


//	THREAD_MODE_BACKGROUND_BEGIN
//	THREAD_PRIORITY_BELOW_NORMAL
//	THREAD_PRIORITY_NORMAL
//	THREAD_PRIORITY_TIME_CRITICAL;
//	REALTIME_PRIORITY_CLASS



int *pPrimeArray = NULL;
int iPrimes = 1;



DWORD WINAPI ComputePrimes (LPVOID);
CRITICAL_SECTION critical_sec;
HANDLE aThreads[nThreads];
long   aNumPrimes[nThreads];

ULONGLONG t1=0, t2=0;


// Forward declarations of functions included in this code module:
ATOM				MyRegisterClass(HINSTANCE hInstance);
BOOL				InitInstance(HINSTANCE, int);
LRESULT CALLBACK	WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK	About(HWND, UINT, WPARAM, LPARAM);
DWORD   WINAPI      ComputePrimes(LPVOID idx);


//
//  PURPOSE:  Create the threads
void DoThreads()
{
	int Max = 0;
	int i;

	t1 = GetTickCount64();
	t2 = t1;
	Max = nPrimes;

	//Allocate memory for the main thread's elements and initialize
	pPrimeArray = (int *) malloc(sizeof (int) * (Max+1));
	for (i = 0; i<=Max; i++)
		pPrimeArray[i] = 0;
	//Set the first prime number
	pPrimeArray[1] = 2;

	InitializeCriticalSection(&critical_sec);

	//create threads
	for (int i=0; i < nThreads; i++)
		if((aThreads[i]=CreateThread (NULL, 0, &ComputePrimes,
			(LPVOID)(i+1),0,NULL)) == NULL)
			;
}


//
//  PURPOSE: Threadproc for computing primes

DWORD WINAPI ComputePrimes(LPVOID lpParam)
{
	int currPrime = 0, i, sqroot;
	BOOL isPrime = TRUE;

	LPVOID lp = lpParam; //int i = (*(int*)lpParam)-1;
	int iThread = reinterpret_cast<int>(lp) - 1;
	SetThreadPriority(GetCurrentThread(), priority);
	
	while (TRUE)
	{
		isPrime = TRUE;
		t2 = GetTickCount64();
		if (iPrimes <= nPrimes)
		{
			EnterCriticalSection (&critical_sec);
			{
				currPrime = iPrimes;
				iPrimes++;

			}
			LeaveCriticalSection (&critical_sec);
			sqroot = (int) sqrt ((double)currPrime)+1;
			for (i = 2; i<= sqroot; i++)
			{
				if((currPrime%i) == 0)
				{
					isPrime = FALSE;
					break;
				}
			}
			EnterCriticalSection (&critical_sec);
			{
				if(isPrime)
				{
					pPrimeArray[currPrime] = currPrime;
					aNumPrimes[iThread]++;
				}
			}
			LeaveCriticalSection (&critical_sec);
		}
		else
		{ 
			// Leave
			t2 = GetTickCount64();
			return 0;
		}
	}
	return 0;
}

//
//  PURPOSE: Terminate all the threads and free memory
void EndThreads()
{
	for (int i=0; i < nThreads; i++)
		CloseHandle (aThreads[i]);
	// free allocated memory
	if(pPrimeArray)
	{
		free(pPrimeArray);
		pPrimeArray = NULL;
	}
}

int APIENTRY _tWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPTSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

 	// TODO: Place code here.
	MSG msg;
	HACCEL hAccelTable;

	// Initialize global strings
	LoadString(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadString(hInstance, IDC_EX1_THREAD_SDK, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// Perform application initialization:
	if (!InitInstance (hInstance, nCmdShow))
	{
		return FALSE;
	}

	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_EX1_THREAD_SDK));

	DoThreads();

	// Main message loop:
	while (GetMessage(&msg, NULL, 0, 0))
	{
		if (msg.message = WM_CLOSE)
			continue;

		TranslateMessage(&msg);
		
		DispatchMessage(&msg);
	
	}

	WaitForMultipleObjects(nThreads, aThreads, true, INFINITE);
	EndThreads();
	return (int) msg.wParam;
}

//
//  FUNCTION: MyRegisterClass()
//
//  PURPOSE: Registers the window class.
//
ATOM MyRegisterClass(HINSTANCE hInstance)
{
	WNDCLASSEX wcex;

	wcex.cbSize = sizeof(WNDCLASSEX);

	wcex.style			= CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc	= WndProc;
	wcex.cbClsExtra		= 0;
	wcex.cbWndExtra		= 0;
	wcex.hInstance		= hInstance;
	wcex.hIcon			= LoadIcon(hInstance, MAKEINTRESOURCE(IDI_EX1_THREAD_SDK));
	wcex.hCursor		= LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+1);
	wcex.lpszMenuName	= MAKEINTRESOURCE(IDC_EX1_THREAD_SDK);
	wcex.lpszClassName	= szWindowClass;
	wcex.hIconSm		= LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

	return RegisterClassEx(&wcex);
}

//
//   FUNCTION: InitInstance(HINSTANCE, int)
//
//   PURPOSE: Saves instance handle and creates main window
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
   HWND hWnd;

   hInst = hInstance; // Store instance handle in our global variable

   hWnd = CreateWindow(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
      CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, NULL, NULL, hInstance, NULL);

   if (!hWnd)
   {
      return FALSE;
   }

   ShowWindow(hWnd, nCmdShow);
   UpdateWindow(hWnd);

   return TRUE;
}

//
//  FUNCTION: WndProc(HWND, UINT, WPARAM, LPARAM)
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	int wmId, wmEvent;
	PAINTSTRUCT ps;
	HDC hdc;
	static int ic = 0, c = 0;

	switch (message)
	{
	case WM_CREATE:
		SetTimer(hWnd, 0, 500, 0);
		break;


	case WM_RBUTTONDOWN:
		ic = ++ic % 3;
		if (ic == 0) c = RGB(255, 0, 0); else if (ic == 1) c=RGB(0, 255, 0); else c = RGB(0, 0, 255);
		break;


	case WM_TIMER:
		RECT rect;  // ONLY INVALIDATE THE LEFT HALF OF THE SCREEB !!!!
		GetClientRect(hWnd, &rect);
		rect.right = rect.right/2;
		InvalidateRect(hWnd, &rect, true);
		break;
	case WM_COMMAND:
		wmId    = LOWORD(wParam);
		wmEvent = HIWORD(wParam);
		// Parse the menu selections:
		switch (wmId)
		{
		case IDM_ABOUT:
			DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
			break;
		case IDM_EXIT:
			DestroyWindow(hWnd);
			break;
		default:
			return DefWindowProc(hWnd, message, wParam, lParam);
		}
		break;
	case WM_PAINT:
		{
		hdc = BeginPaint(hWnd, &ps);
		// TODO: Add any drawing code here...
		TCHAR s[100];

		HFONT 
		hFont = CreateFont(28,0,0,0,FW_DONTCARE,FALSE,FALSE,FALSE,DEFAULT_CHARSET,OUT_OUTLINE_PRECIS,
                CLIP_DEFAULT_PRECIS,CLEARTYPE_QUALITY, VARIABLE_PITCH,TEXT("Times New Roman"));     // A NIGHTMARE OF PARAMETERS :=)

        SelectObject(hdc, hFont);
		SetTextColor(hdc, RGB(0,0,255));

		wsprintf(s, _T("Time %ld"), t2-t1);
		TextOut(hdc, 50, 50, s, lstrlenW(s));
		for (int i=0; i < nThreads; i++)
		{
			wsprintf(s, _T("Thread nr %d, number of primes %ld"), i, aNumPrimes[i]);
			TextOut(hdc, 50, 100+i*30, s, lstrlenW(s));
		}
		EndPaint(hWnd, &ps);
		break;
		}
	case WM_MOUSEMOVE:
		{	// NOT - HERE WE DRAW OUTSIDE WM_PAINT
		int x = LOWORD(lParam);
		int y = HIWORD(lParam);
		static POINT ptPrevious;

        hdc = GetDC(hWnd);  // Get DC outside WM_PAINT
		HPEN hPen = CreatePen(0, 5, c);
		HGDIOBJ sav = SelectObject(hdc, hPen);

        MoveToEx(hdc, ptPrevious.x, ptPrevious.y, NULL); 

		ptPrevious.x = LOWORD(lParam),
		ptPrevious.y = HIWORD(lParam);
		if ((GetKeyState(VK_LBUTTON) & 0x100) != 0)
		{
			LineTo(hdc, ptPrevious.x , ptPrevious.y );
		}
		SelectObject(hdc, sav);
		DeleteObject(hPen);

        ReleaseDC(hWnd, hdc); 
		}
		break;

	case WM_DESTROY:
		KillTimer(hWnd, 0);
		PostQuitMessage(0);
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


