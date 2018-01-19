#pragma once
class Trafikklys {

public:

	Trafikklys(HDC hdc, int x, int y, int hState);

	~Trafikklys();

	void createTrafficLights();

private:
	HDC hDC;
	int X;
	int Y;
	int state;
};
