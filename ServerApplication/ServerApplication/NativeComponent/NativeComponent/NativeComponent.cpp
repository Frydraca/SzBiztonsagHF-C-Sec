// NativeComponent.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <fstream>
#include <sstream>
#include <vector>
#include "CAFFParser.h"
#include "ConsoleParameterParser.h"
//#include "DecipherCAFF.h"

std::vector<char> HexToBytes(const std::string& hex) {
    std::vector<char> bytes;

    for (unsigned int i = 0; i < hex.length(); i += 2) {
        std::string byteString = hex.substr(i, 2);
        char byte = (char)strtol(byteString.c_str(), NULL, 16);
        bytes.push_back(byte);
    }

    return bytes;
}


int main(int argc, char* argv[])
{
    const char* path_of_file = argv[1];
    char* preview_path = argv[2];

    char fname[256];

    std::ifstream infile;
    infile.open(path_of_file, std::ios::binary | std::ios::in);

    CAFFParser cP = CAFFParser();
    CAFFParser::CAFFMetadata parseResult = cP.ReturnPreview(&infile, fname, preview_path);

    std::cout << "--START--" << std::endl;
    std::cout << "{" << std::endl << "\t\"CreationDate\": \"" << parseResult.CreationDate << "\"," << std::endl << "\t\"Creator\": \"" << parseResult.Creator << "\"," << std::endl << "\t\"PreviewCaption\": \"" << parseResult.PreviewCaption << "\"," << std::endl << "\t\"PreviewTags\": \"" << parseResult.PreviewTags << "\"," << std::endl << "\t\"PreviewPath\": \"" << parseResult.PreviewPath << "\"" << std::endl << "}" << std::endl;
    std::cout << "--END--";

    infile.close();
}
