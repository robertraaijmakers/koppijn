#pragma once

#include "KaroEngine.h"
#include <string.h>

using namespace std;
using namespace System;

namespace KaroEngine {
	ref class KaroEngineWrapper
	{


	private:
		KaroEngine *_karoEngine; // native pointer
	public:
		/*property String ^Name
		{
			String ^get();
			void set(String ^);
		}*/

		KaroEngineWrapper(void);
		~KaroEngineWrapper(void);
		//PersonWrapper(String ^, int);
		//~PersonWrapper(void);

		//String^ ToString() new;
	};
}