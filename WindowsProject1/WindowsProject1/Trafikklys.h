#pragma once
class Trafikklys {

public:

	Trafikklys(int left, int top, int right, int bottom, int hState);

	~Trafikklys();

	void createTrafficLight(HDC hdc);

private:
	int leftPos;
	int topPos;
	int rightPos;
	int bottomPos;
	int state;
};
