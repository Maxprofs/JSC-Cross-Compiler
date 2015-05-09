/************************************************************************************

Filename    :   OvrApp.h
Content     :   Trivial use of VrLib
Created     :   February 10, 2014
Authors     :   John Carmack

Copyright   :   Copyright 2014 Oculus VR, LLC. All Rights reserved.


************************************************************************************/



// http://research.engineering.wustl.edu/~beardj/Mixed_C_C++.html

#include "jni.h"


#ifdef __cplusplus

#ifndef OVRAPP_H
#define OVRAPP_H

#include "App.h"
#include "ModelView.h"

class OvrApp : public OVR::VrAppInterface
{
public:
						OvrApp();
    virtual				~OvrApp();

	virtual void 		ConfigureVrMode( ovrModeParms & modeParms );

	virtual void		OneTimeInit( const char * fromPackage, const char * launchIntentJSON, const char * launchIntentURI );
	virtual void		OneTimeShutdown();
	virtual Matrix4f 	DrawEyeView( const int eye, const float fovDegrees );
	virtual Matrix4f 	Frame( VrFrame vrFrame );
	virtual void		Command( const char * msg );

	OvrSceneView		Scene;
};

#endif

extern "C" {
char* cxxGetString();
jlong cxxSetAppInterface(JNIEnv *jni, jclass clazz, jobject activity,
						 jstring fromPackageName, jstring commandString, jstring uriString);
}

#else
// jsc is not generating c++ but is generating c and java

// can we call C++ from C?
// did we update Android.mk
char* cxxGetString();
jlong cxxSetAppInterface(JNIEnv *jni, jclass clazz, jobject activity,
						 jstring fromPackageName, jstring commandString, jstring uriString);

#endif