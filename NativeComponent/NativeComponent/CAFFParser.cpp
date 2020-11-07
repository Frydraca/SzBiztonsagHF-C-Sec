#include "CAFFParser.h"
#include <iostream>
#include "lodepng.h"


CAFFParser::IdAndLength CAFFParser::GetBlockIdAndLength(std::ifstream* infile)
{
	IdAndLength ret;
	char id[1];
	infile->read(id, sizeof(id));
	ret.id = int(id[0]);

	std::cout << "id: " << ret.id << std::endl;

	char length[8];
	infile->read(length, sizeof(length));
	std::memcpy(&ret.length, length, sizeof(int));

	std::cout << "length: " << ret.length << std::endl;
	return ret;
}

void CAFFParser::ParseCAFFAnimation(std::ifstream* infile, char* preview)
{
	char duration[8];
	infile->read(duration, sizeof(duration));

	char magic[4];
	infile->read(magic, sizeof(magic));
	
	char header_size[8];
	infile->read(header_size, sizeof(header_size));
	int header_size_value;
	std::memcpy(&header_size_value, header_size, sizeof(int));

	ImageMetadata metadata;
	char content_size[8];
	infile->read(content_size, sizeof(content_size));
	std::memcpy(&metadata.content_size, content_size, sizeof(int));

	char width[8];
	infile->read(width, sizeof(width));
	std::memcpy(&metadata.width, width, sizeof(int));

	char height[8];
	infile->read(height, sizeof(height));
	std::memcpy(&metadata.height, height, sizeof(int));

	char* skip = new char[header_size_value - 44];
	infile->read(skip, (header_size_value-44));
	delete[] skip;

	char* image_bytes = new char[metadata.content_size];
	infile->read(image_bytes, metadata.content_size);
	unsigned char* unsigned_image_bytes = new unsigned char[metadata.content_size];
	for (int i = 0;i < metadata.content_size;i++) {
		unsigned_image_bytes[i] = ((unsigned char)(image_bytes[i]));
	}
	delete[] image_bytes;
	std::vector<unsigned char> ImageBuffer;

	lodepng::encode(ImageBuffer,unsigned_image_bytes,metadata.width,metadata.height,LCT_RGB,8U);
	lodepng::save_file(ImageBuffer,"c:/BME/C-Sec/SzBiztonsagHF-C-Sec/preview.png");
}

void CAFFParser::ReturnPreview(std::ifstream* infile, char* preview)
{
	IdAndLength headerBlock = GetBlockIdAndLength(infile);

	if (headerBlock.id == 1) {
		char* header_data = new char[headerBlock.length];
		infile->read(header_data, headerBlock.length);
		int num_anim_value;
		std::memcpy(&num_anim_value, &header_data[12], sizeof(int));
		std::cout << num_anim_value << std::endl;

		if (num_anim_value > 0) {
			while (true) {
				IdAndLength actBlock = GetBlockIdAndLength(infile);

				if (actBlock.id != 3) {
					char* skip = new char[actBlock.length];
					infile->read(skip, actBlock.length);
					delete[] skip;
				}
				else {
					ParseCAFFAnimation(infile, preview);
					return;
				}
			}
		}
	}
}
