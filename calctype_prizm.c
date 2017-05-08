#include "calctype.h"
#include "fxcg\display.h"

unsigned char* CalcType_DefaultVRAM() {
	// direct to screen
	return (unsigned char*) GetVRAMAddress();
}

unsigned int CalcType_DefaultPitch() {
	// screen width
	return 384;
}

#define RED_PART(x) ((x & 0xF800) >> 11)
#define GREEN_PART(x) ((x & 0x07E0) >> 5)
#define BLUE_PART(x) (x & 0x001F)

inline void CalcType_DrawGlyph(CalcTypeCharData* glyph, int x, int y, unsigned char* vram, unsigned int pitch) {
	unsigned short* drawTo = ((unsigned short*)vram) + x + y * pitch;
	unsigned char* data = glyph->data;
	int drawIncrement = pitch - glyph->width;

	unsigned short drawColor = 0xFFFF;
	unsigned short drawRed = RED_PART(drawColor);
	unsigned short drawGreen = GREEN_PART(drawColor);
	unsigned short drawBlue = BLUE_PART(drawColor);

	for (int gy = 0; gy < glyph->height; gy++) {
		for (int gx = 0; gx < glyph->width; gx++, drawTo++, data++) {
			// glyph data is packed 3:4:1
			int col = *drawTo;
			int dataByte = data[0];
			int screenAmt = ~dataByte;

			// interpolate has a lot of divides in it currently (probably can be mitigated with something clever)
			col = //drawColor;
				(((RED_PART(col) * ((screenAmt & 0xE0) >> 5) + drawRed * ((dataByte & 0xE0) >> 5)) / 7) << 11) |
				(((GREEN_PART(col) * ((screenAmt & 0x1E) >> 1) + drawGreen * ((dataByte & 0x1E) >> 1)) / 15) << 5) |
				(((BLUE_PART(col) * (screenAmt & 1) + drawBlue * (dataByte & 1))));

			*drawTo = col;
		}
		drawTo += drawIncrement;
	}
}