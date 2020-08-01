//
//  Created by weRplay on 6/28/13.
//	Modified 12/09/13
//	fhq
//	weRplay


//#import "AppController.h"
#import "WRPiOSUtilities.h"
#import "UnityInterface.h"


extern "C"
{	
	void _showLoginPasswordDialogWithTitleAndMessageAndCancelTitleAndSecondTitleAndThirdTitle( char *boxTitle, char *boxMessage,  char *cancelTitle, char *secondTitle, char *thirdTitle, int idTag );
	
	void _showSecureTextFieldDialogWithTitleAndMessageAndCancelTitleAndSecondTitleAndThirdTitle( char *boxTitle, char *boxMessage, char *cancelTitle, char *secondTitle, char *thirdTitle, int idTag );
	
	void _showNormalTextFieldDialogWithTitleAndMessageAndCancelTitleAndSecondTitleAndThirdTitle( char *boxTitle, char *boxMessage, char *cancelTitle, char *secondTitle, char *thirdTitle, int idTag );

	void _showDialogWithTitleAndMessageAndCancelTitleAndSecondTitleAndThirdTitleAndFourthTitleAndFifthTitle( char *boxTitle, char *boxMessage, char *cancelTitle, char *secondTitle, char *thirdTitle, char *fourthTitle, char *fifthTitle, int idTag );
	
	void _showDialogWithTitleAndMessageAndCancelTitleAndOtherButtonTitlesArray( char *boxTitle, char *boxMessage, char *cancelTitle, char *otherButtonTitlesArray[], int idTag );
	
	void _setObjectName( char *givenName );
}

void _showLoginPasswordDialogWithTitleAndMessageAndCancelTitleAndSecondTitleAndThirdTitle( char *boxTitle, char *boxMessage,  char *cancelTitle, char *secondTitle, char *thirdTitle, int idTag )
{
	[ WRPiOSUtilities setUnityCallObject: [ UnityInterface sharedInstance ] ];
	 
	[ WRPiOSUtilities showDialogBoxWithLoginPasswordFieldsAndTitle: boxTitle Message: boxMessage CancelButtonTitle: cancelTitle SecondButtonTitle: secondTitle ThirdButtonTitle: thirdTitle SetTag: idTag ];
}

void _showSecureTextFieldDialogWithTitleAndMessageAndCancelTitleAndSecondTitleAndThirdTitle( char *boxTitle, char *boxMessage, char *cancelTitle, char *secondTitle, char *thirdTitle, int idTag )
{
	[ WRPiOSUtilities setUnityCallObject: [ UnityInterface sharedInstance ] ];
	
	[ WRPiOSUtilities showDialogBoxWithSecureTextFieldAndTitle: boxTitle Message: boxMessage CancelButtonTitle: cancelTitle SecondButtonTitle: secondTitle ThirdButtonTitle: thirdTitle SetTag: idTag ];
}

void _showNormalTextFieldDialogWithTitleAndMessageAndCancelTitleAndSecondTitleAndThirdTitle( char *boxTitle, char *boxMessage, char *cancelTitle, char *secondTitle, char *thirdTitle, int idTag )
{
	[ WRPiOSUtilities setUnityCallObject: [ UnityInterface sharedInstance ] ];
	
	[ WRPiOSUtilities showDialogBoxWithNormalTextFieldAndTitle: boxTitle Message: boxMessage CancelButtonTitle: cancelTitle SecondButtonTitle: secondTitle ThirdButtonTitle: thirdTitle SetTag: idTag ];
}

void _showDialogWithTitleAndMessageAndCancelTitleAndSecondTitleAndThirdTitleAndFourthTitleAndFifthTitle( char *boxTitle, char *boxMessage, char *cancelTitle, char *secondTitle, char *thirdTitle, char *fourthTitle, char *fifthTitle, int idTag )
{
	[ WRPiOSUtilities setUnityCallObject: [ UnityInterface sharedInstance ] ];
	
	[ WRPiOSUtilities showDialogBoxWithTitle: boxTitle Message: boxMessage CancelButtonTitle: cancelTitle SecondButtonTitle: secondTitle ThirdButtonTitle: thirdTitle FourthButtonTitle: fourthTitle fifthButtonTitle: fifthTitle SetTag: idTag ];
}

void _showDialogWithTitleAndMessageAndCancelTitleAndOtherButtonTitlesArray( char *boxTitle, char *boxMessage, char *cancelTitle, char *otherButtonTitlesArray[], int idTag )
{
	[ WRPiOSUtilities setUnityCallObject: [ UnityInterface sharedInstance ] ];
	
	[ WRPiOSUtilities showDialogBoxOfType: nil WithTitle: boxTitle Message: boxMessage CancelButtonTitle: cancelTitle OtherButtonTitles: otherButtonTitlesArray SetTag: idTag ];
}

void _setObjectName( char *givenName )
{
	[ WRPiOSUtilities setUnityCallObject: [ UnityInterface sharedInstance ] ];
	
	[ WRPiOSUtilities setObjectName: givenName ];
}