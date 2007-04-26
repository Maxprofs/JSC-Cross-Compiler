/* DO NOT EDIT THIS FILE - it is machine generated */
#include <jni.h>
/* Header for class jni_CMalloc */

#ifndef _Included_jni_CMalloc
#define _Included_jni_CMalloc
#ifdef __cplusplus
extern "C" {
#endif
/*
 * Class:     jni_CMalloc
 * Method:    malloc
 * Signature: (I)J
 */
JNIEXPORT jlong JNICALL Java_jni_CMalloc_malloc
  (JNIEnv *, jclass, jint);

/*
 * Class:     jni_CMalloc
 * Method:    free
 * Signature: (J)V
 */
JNIEXPORT void JNICALL Java_jni_CMalloc_free
  (JNIEnv *, jclass, jlong);

/*
 * Class:     jni_CMalloc
 * Method:    sizeof_jint
 * Signature: ()I
 */
JNIEXPORT jint JNICALL Java_jni_CMalloc_sizeof_1jint
  (JNIEnv *, jclass);

/*
 * Class:     jni_CMalloc
 * Method:    sizeof_jbyte
 * Signature: ()I
 */
JNIEXPORT jint JNICALL Java_jni_CMalloc_sizeof_1jbyte
  (JNIEnv *, jclass);

/*
 * Class:     jni_CMalloc
 * Method:    sizeof_jlong
 * Signature: ()I
 */
JNIEXPORT jint JNICALL Java_jni_CMalloc_sizeof_1jlong
  (JNIEnv *, jclass);

#ifdef __cplusplus
}
#endif
#endif
