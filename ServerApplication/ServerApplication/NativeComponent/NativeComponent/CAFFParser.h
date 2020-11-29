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
	
	public:
		struct CAFFMetadata {
			std::string CreationDate;
			std::string Creator;
			std::string PreviewCaption;
			std::string PreviewTags;
			std::string PreviewPath;
		} typedef CAFFMetadata;

	private:
		IdAndLength GetBlockIdAndLength(std::ifstream* infile);
		void ParseCAFFAnimation(std::ifstream* infile, char* fname, CAFFMetadata* caffMetadata, char* previewPath);
		void ParseCAFFCredit(std::ifstream* infile, CAFFMetadata* caffMetadata);

	public:
		CAFFMetadata ReturnPreview(std::ifstream* infile, char* fname, char* previewPath);

};

