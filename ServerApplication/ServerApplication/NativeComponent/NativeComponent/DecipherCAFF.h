#pragma once
#include <iostream>
#include <fstream>
#include <sstream>
#include <openssl/aes.h>
class DecipherCAFF
{
	public:
		void DecryptFile(std::ifstream* infile, std::stringstream* decStream, unsigned char* aes_key, unsigned char* iv);
};

