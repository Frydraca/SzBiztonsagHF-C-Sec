#pragma once

#include <string>

using namespace std;

namespace ConsoleParameterParserNamespace {
	class ConsoleParameterParser
	{
	public:
			string filename;
			string hashcode;
			string mode;

	public:
		void handleConsoleParameters(int argc, char* argv[]);
	private:
		void setParameter(string param, string value);
	};
}

