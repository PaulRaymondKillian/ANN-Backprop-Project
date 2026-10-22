#ifndef BACKPROP_H
#define BACKPROP_H

//Include Files below
//#include "stdafx.h"
#include <iostream>
#include <vector>
#include <fstream>
#include <string>
#include <time.h>
#include <math.h>
#include <iomanip>
#include <sstream>
#include <windows.h>	//Color Console

using namespace std;

class Backprop
{

public:
	
	Backprop( void );								//Default Constructor
	Backprop( string filepath );		//Constructor with 2 parameters
	Backprop( string *pParms, vector< vector<double> > pExemplarInputs, vector< vector<double> > pExemplarOutputs, int pExemplarCar, int pCountInputNeurons, int pCountOutputNeurons);
	//Backprop::Backprop( string *pParms, double **pExemplarInputs, double **pExemplarOutputs, int pExemplarCount, int pCountInputNeurons, int pCountOutputNeurons);
	~Backprop( void );									//Destructor

	//////////////////////////////////////////////////////////////////////////
	// Begin Method PROTOS

	void ImportBPROPFile( string filepath );
	void ImportBPROP2File( string filepath );
	void RandomizeTheIndex( void );
	void Tokenize(const string& str, vector<string>& tokens, const string& delimiters);
	void InitializeArrays( void );
	void RandomizeWeightAndBiasArrays();
	void ImportExemplarSet( void );
	//void InitializeArrays( void );
	int numRowsInTextFile( string filepath );
	double RandomWeight( double maxValue );
	int RandomNumber( int MaxNumber );
	double AbsVal( double value );
	double CalcFitness( void );
	void OutputBPROPFile( string filename);
	void OutputBPROP2File( string filename);
	void OutputNetwork( string fileName );
	void OutputNetworkOutput( string fileName );
	void SaveData( void );
	void DeconstructCurrentDirectory( string filepath );
	string GenerateSaveName( void );
	void SelectInputFile( void );
	void LoadBPROPFile( void );
	void LoadBPROP2File( void );
	void ImportANNFile( void );
	void OutputWeightsToConsole( void );	//Debugging Purposes
	void OutputNeuronsToConsole( void );	//Debugging Purposes
	int RuntimeConfigUpdate( void );
	double VerifyValuePositive( double value );


	// END METHOD PROTOS
	//////////////////////////////////////////////////////////////////////////



	HANDLE  hConsole; //Color Console
	enum { Blank, DarkBlue, DarkGreen, DarkLightBlue, DarkRed, Purple, DarkYellow, White, Grey, Blue, Green, LightBlue, Red, Pink, Yellow, BrightWhite };	//Color Console

	/////////////////////////////////////////////////////////////////////////////////////
	//DECLARE VARIABLES

	//CONST VALUES
	int static const VALUE = 0;
	int static const BIAS = 1;
	int static const DELTA = 2;

	//Exemplar Information
	string DataSetFile;
	int countExemplarSets;
	double **InputExemplars;
	double **OutputExemplars;

	//Architecture
	string ANNFileName;
	int countInputNeurons;
	int countHiddenNeurons;
	int countHiddenLayers;
	int countOutputNeurons;

	//Neuron Arrays
	double *inputValues;
	double ***hiddenValues;
	double **outputValues;

	//Weights Arrays
	double **ihWeights;
	double ***hhWeights;
	double **hoWeights;

	//Training Configuration
	int EpochsPerInterval;
	int Intervals;
	double MaxWeightValue;
	double MaxBiasValue;
	double Rho;
	double RhoMultiplier;
	int RhoInterval;
	bool Momentum;
	double MomentumScaler;
	int SimAnnealMaxPercent;
	int SimAnnealInterval;

	//Misc Variables
	string FileTitle;
	int SaveInterval;
	int ConsoleLoggingInterval;
	int *RandomArray;
	time_t TimeIntervalStart;
	time_t TimeIntervalEnd;
	time_t TimeTrainingStart;
	time_t TimeTrainingEnd;
	double PreviousFitness;
	double CurrentFitness;
	int static const LargeSetPrecisionValue = 15;
	int static const SmallSetPrecisionValue = 5;
	string CurrentDirectory;
	string InputFile;
	//vector<string> AppPreviousEntries(10);
	int countPreviousEntries;

	// END VARIABLE DECLARATIONS
	//////////////////////////////////////////////////////////////////////////

};

#endif