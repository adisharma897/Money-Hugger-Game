//
//  WRPiOSUtilities.h
//  iOSNativeInterface
//
//  Created by fhq on 12/2/13.
//  Copyright (c) 2013 fhq. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface WRPiOSUtilities : NSObject
{
	
}

+( void ) showDialogBoxOfType: ( int )boxType WithTitle: ( char * )boxTitle Message: ( char * )boxMessage CancelButtonTitle: ( char * )cancelTitle OtherButtonTitles: ( char *[] )otherButtonTitlesArray SetTag: ( int )idTag;


+( void ) showDialogBoxWithLoginPasswordFieldsAndTitle: ( char * )boxTitle Message: ( char * )boxMessage CancelButtonTitle: ( char * )cancelTitle SecondButtonTitle: ( char * )secondTitle ThirdButtonTitle: ( char * )thirdTitle SetTag: ( int )idTag;
+( void ) showDialogBoxWithSecureTextFieldAndTitle: ( char * )boxTitle Message: ( char * )boxMessage CancelButtonTitle: ( char * )cancelTitle SecondButtonTitle: ( char * )secondTitle ThirdButtonTitle: ( char * )thirdTitle SetTag: ( int )idTag;
+( void ) showDialogBoxWithNormalTextFieldAndTitle: ( char * )boxTitle Message: ( char * )boxMessage CancelButtonTitle: ( char * )cancelTitle SecondButtonTitle: ( char * )secondTitle ThirdButtonTitle: ( char * )thirdTitle SetTag: ( int )idTag;
+( void ) showDialogBoxWithTitle: ( char * )boxTitle Message: ( char * )boxMessage CancelButtonTitle: ( char * )cancelTitle SecondButtonTitle: ( char * )secondTitle ThirdButtonTitle: ( char * )thirdTitle FourthButtonTitle: ( char * )fourthTitle fifthButtonTitle: ( char * )fifthTitle SetTag: ( int )idTag;

+( void ) setObjectName: ( char * )nameOfObject;
+( void ) setUnityCallObject: ( id )givenObject;

@end