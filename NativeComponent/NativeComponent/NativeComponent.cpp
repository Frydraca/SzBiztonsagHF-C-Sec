// NativeComponent.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <fstream>
#include <sstream>
#include "CAFFParser.h"
#include "ConsoleParameterParser.h"

int main(int argc, char* argv[])
{
    const char* path_of_file = "../../TestResources/3.caff";
    char fname[_MAX_FNAME];

    errno_t error = _splitpath_s(path_of_file,
        NULL, 0,
        NULL, 0,
        fname, _MAX_FNAME,
        NULL, 0);

    std::ifstream infile;
    infile.open(path_of_file, std::ios::binary | std::ios::in);


    CAFFParser cP = CAFFParser();
    CAFFParser::CAFFMetadata parseResult = cP.ReturnPreview(&infile, fname);

    std::cout << "{" << std::endl << "\t\"CreationDate\": \"" << parseResult.CreationDate << "\"," << std::endl << "\t\"Creator\": \"" << parseResult.Creator << "\"," << std::endl << "\t\"PreviewCaption\": \"" << parseResult.PreviewCaption << "\"," << std::endl << "\t\"PreviewTags\": \"" << parseResult.PreviewTags << "\"," << std::endl << "\t\"PreviewPath\": \"" << parseResult.PreviewPath << "\"" << std::endl << "}";

    infile.close();
}
