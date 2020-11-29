#pragma once
#include <fstream>
#include <iostream>
#include "lodepng.h"
#include <time.h>
#include <algorithm>
#include <sstream>

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
	
	public:
		struct CAFFMetadata {
			std::string CreationDate;
			std::string Creator;
			std::string PreviewCaption;
			std::string PreviewTags;
			std::string PreviewPath;
		} typedef CAFFMetadata;

	private:
		IdAndLength GetBlockIdAndLength(std::istream* infile);
		void ParseCAFFAnimation(std::istream* infile, char* fname, CAFFMetadata* caffMetadata, char* previewPath);
		void ParseCAFFCredit(std::istream* infile, CAFFMetadata* caffMetadata);

	public:
		CAFFMetadata ReturnPreview(std::istream* infile, char* fname, char* previewPath);

};

