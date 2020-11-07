#include "CAFFParser.h"
#include <iostream>


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
					std::cout << "KowaBungaaaaaaaa!!!" << std::endl;

					return;
				}
			}
		}
	}
}
