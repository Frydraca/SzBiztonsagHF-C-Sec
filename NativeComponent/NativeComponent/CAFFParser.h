#pragma once
#include <fstream>
class CAFFParser
{
	struct IdAndLength {
		int id;
		int length;
	} typedef IdAndLength;

	struct ImageMetadata {
		int content_size;
		int width;
		int height;
	} typedef ImageMetadata;

	private:
		IdAndLength GetBlockIdAndLength(std::ifstream* infile);
		void ParseCAFFAnimation(std::ifstream* infile, char* preview);

	public:
		void ReturnPreview(std::ifstream* infile, char* preview);
};

