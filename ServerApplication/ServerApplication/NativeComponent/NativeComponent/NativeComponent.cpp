// NativeComponent.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <iomanip>
#include <string>
#include <fstream>
#include <sstream>
#include "CAFFParser.h"
#include "ConsoleParameterParser.h"
#include <openssl/sha.h>

std::string sha256(const std::string str)
{
    unsigned char hash[SHA256_DIGEST_LENGTH];
    SHA256_CTX sha256;
    SHA256_Init(&sha256);
    SHA256_Update(&sha256, str.c_str(), str.size());
    SHA256_Final(hash, &sha256);
    std::stringstream ss;
    for (int i = 0; i < SHA256_DIGEST_LENGTH; i++)
    {
        ss << hex << setw(2) << setfill('0') << (int)hash[i];
    }
    return ss.str();
}


int main(int argc, char* argv[])
{
    const char* path_of_file = argv[1];
    char* preview_path = argv[2];
    char fname[_MAX_FNAME];

    std::cout << sha256("alma") << std::endl;
    std::cout << sha256("1234567890_2") << std::endl;
    std::cout << sha256("1234567890_3") << std::endl;
    std::cout << sha256("1234567890_4") << std::endl;

    errno_t error = _splitpath_s(path_of_file,
        NULL, 0,
        NULL, 0,
        fname, _MAX_FNAME,
        NULL, 0);

    std::ifstream infile;
    infile.open(path_of_file, std::ios::binary | std::ios::in);

    CAFFParser cP = CAFFParser();
    CAFFParser::CAFFMetadata parseResult = cP.ReturnPreview(&infile, fname, preview_path);


    std::cout << "--START--" << std::endl;
    std::cout << "{" << std::endl << "\t\"CreationDate\": \"" << parseResult.CreationDate << "\"," << std::endl << "\t\"Creator\": \"" << parseResult.Creator << "\"," << std::endl << "\t\"PreviewCaption\": \"" << parseResult.PreviewCaption << "\"," << std::endl << "\t\"PreviewTags\": \"" << parseResult.PreviewTags << "\"," << std::endl << "\t\"PreviewPath\": \"" << parseResult.PreviewPath << "\"" << std::endl << "}" << std::endl;
    std::cout << "--END--";

    infile.close();
}
