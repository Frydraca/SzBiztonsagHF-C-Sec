#include "CAFFParser.h"


const std::string currentDateTime() {
	time_t     now = time(0);
	struct tm  tstruct;
	char       buf[80];
	localtime_s(&tstruct, &now);

	strftime(buf, sizeof(buf), "%Y_%m_%d_%X", &tstruct);
	std::string ret(buf);
	std::replace(ret.begin(), ret.end(), ':', '_');

	return ret;
}

CAFFParser::IdAndLength CAFFParser::GetBlockIdAndLength(std::ifstream* infile)
{
	IdAndLength ret;
	char id[1];
	infile->read(id, sizeof(id));
	ret.id = int(id[0]);

	char length[8];
	infile->read(length, sizeof(length));
	std::memcpy(&ret.length, length, sizeof(int));

	return ret;
}

void CAFFParser::ParseCAFFAnimation(std::ifstream* infile, char* fname, CAFFMetadata* caffMetadata, char* previewPath)
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

	char c;
	std::stringstream caption_stream;
	while (infile->get(c)) {
		if (c == '\n') {
			break;
		}
		caption_stream << c;
	}
	caffMetadata->PreviewCaption = caption_stream.str();

	int tagslength = header_size_value - 36 - caffMetadata->PreviewCaption.length() -1;
	char* tags = new char[tagslength];
	infile->read(tags, tagslength);
	std::stringstream tags_stream;
	for (int i = 0; i < tagslength; i++) {
		tags_stream << ((tags[i] == '\0' && i != tagslength - 1) ? ',' : tags[i] == '\0' ? ';' : tags[i]);
	}
	caffMetadata->PreviewTags = tags_stream.str();


	char* image_bytes = new char[metadata.content_size];
	infile->read(image_bytes, metadata.content_size);
	unsigned char* unsigned_image_bytes = new unsigned char[metadata.content_size];
	for (int i = 0;i < metadata.content_size;i++) {
		unsigned_image_bytes[i] = ((unsigned char)(image_bytes[i]));
	}
	delete[] image_bytes;
	std::vector<unsigned char> ImageBuffer;

	std::string filename_old(fname);
	std::string act_time = currentDateTime();
	std::string preview_path = previewPath + filename_old + "_" + act_time + "_preview.png";

	lodepng::encode(ImageBuffer,unsigned_image_bytes,metadata.width,metadata.height,LCT_RGB,8U);
	lodepng::save_file(ImageBuffer,preview_path);
	caffMetadata->PreviewPath = preview_path;
}

void CAFFParser::ParseCAFFCredit(std::ifstream* infile, CAFFMetadata* caffMetadata)
{
	char year[2];
	infile->read(year, sizeof(year));
	std::string year_value = std::to_string(static_cast<int>(static_cast<unsigned char>(year[0])) | static_cast<int>(static_cast<unsigned char>(year[1])) << 8);
	char month[1];
	infile->read(month, sizeof(month));
	std::string month_value = std::to_string(static_cast<int>(static_cast<unsigned char>(month[0])));
	char day[1];
	infile->read(day, sizeof(day));
	std::string day_value = std::to_string(static_cast<int>(static_cast<unsigned char>(day[0])));
	char hour[1];
	infile->read(hour, sizeof(hour));
	std::string hour_value = std::to_string(static_cast<int>(static_cast<unsigned char>(hour[0])));
	char minute[1];
	infile->read(minute, sizeof(minute));
	std::string minute_value = std::to_string(static_cast<int>(static_cast<unsigned char>(minute[0])));

	caffMetadata->CreationDate = year_value + "-" + month_value + "-" + day_value + "." + hour_value + ":" + minute_value;

	char creator_len[8];
	infile->read(creator_len, sizeof(creator_len));
	int creator_len_value;
	std::memcpy(&creator_len_value, creator_len, sizeof(int));

	char* creator = new char[creator_len_value+1];
	infile->read(creator, creator_len_value);
	creator[creator_len_value] = '\0';
	std::string str_creator(creator);
	caffMetadata->Creator = str_creator;
}

CAFFParser::CAFFMetadata CAFFParser::ReturnPreview(std::ifstream* infile, char* fname, char* previewPath)
{
	IdAndLength headerBlock = GetBlockIdAndLength(infile);

	if (headerBlock.id == 1) {
		char* header_data = new char[headerBlock.length];
		infile->read(header_data, headerBlock.length);
		int num_anim_value;
		std::memcpy(&num_anim_value, &header_data[12], sizeof(int));

		if (num_anim_value > 0) {
			CAFFMetadata caffMetadata;
			int picCount = 0;
			while (!infile->eof()) {
				IdAndLength actBlock = GetBlockIdAndLength(infile);

				if (actBlock.id != 2 && actBlock.id != 3) {
					break;
				}
				if (actBlock.id == 2) {
					ParseCAFFCredit(infile, &caffMetadata);
				}
				if (actBlock.id == 3 && picCount == 0) {
					ParseCAFFAnimation(infile, fname, &caffMetadata, previewPath);
					picCount++;
				}
			}

			return caffMetadata;
		}
	}
}
