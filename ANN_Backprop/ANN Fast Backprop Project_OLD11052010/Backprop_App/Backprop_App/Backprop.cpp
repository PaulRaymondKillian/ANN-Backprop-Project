/*------------------------------------------------------------------------
    Program :           ANN Object
    Written by:         Paul Killian
    Created:            2009
    Description:        Contains all the needed information for an ANN
 -------------------------------------------------------------------------*/

//Include Files below
#include "Backprop.h"


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


Backprop::Backprop( void ) // Default Constructor
{
//Do nothing

}

Backprop::Backprop( string filepath )					//Constructor with 1 parameter
{

}//ANN()


//Backprop::Backprop( string *pParms, vector< vector<double> > pExemplarInputs, vector< vector<double> > pExemplarOutputs, int pExemplarCount, int pCountInputNeurons, int pCountOutputNeurons)
////Backprop::Backprop( string *pParms, double **pExemplarInputs, double **pExemplarOutputs, int pExemplarCount, int pCountInputNeurons, int pCountOutputNeurons)
//{
//    //Public Sub New(ByVal Parms() As String, ByRef pExemplarInputs(,) As Double, ByRef pExemplarOutputs(,) As Double, ByVal pExemplarCount As Integer, ByVal pCountInputNeurons As Integer, ByVal pCountOutputNeurons As Integer)
//    //    _Parms = Parms
//    //    importBackpropParms(_Parms)
//    //    exemplarInputs = pExemplarInputs
//    //    exemplarOutputs = pExemplarOutputs
//    //    exemplarCount = pExemplarCount
//    //    countInputNeurons = pCountInputNeurons
//    //    countOutputNeurons = pCountOutputNeurons
//	
//	//Sleep(500);
//	//cout << endl << time(0) << " - " << clock();
//	//time(NULL) returns time in seconds only, which means that all backprop objects created during the same second time duration
//	//  will all have same random seed, which is kinda stupid, so I added -clock() to mix up the seed value so they are all different.
//	srand ( time(NULL)-clock() );
//	//cout << ", " << time(0) << " - " << clock()<< endl;
//
//	Parms = pParms;
//	importBackpropParms(Parms);
//	exemplarInputs = pExemplarInputs;
//	exemplarOutputs = pExemplarOutputs;
//	VerifyArchitecture();
//	//exemplarCount = pExemplarCount;
//	//countInputNeurons = pCountInputNeurons;
//	//countOutputNeurons = pCountOutputNeurons;
//
//    //    InitializeArrays
//	PrepVectors();
//
//    //    RandomizeWeightAndBiasArrays()
//	RandomizeWeightAndBiasArrays();
//
//    //End Sub
//}

Backprop::~Backprop(void)								//Destructor
{

}





/////////////////////////////////////////////////////////////////////////
// METHODS BEGIN HERE

void Backprop::InitializeArrays( void )
{
	//Initialize RandomArray
	RandomArray = new int[countExemplarSets];
	
	//Initialize Input Values Array
	inputValues = new double[countInputNeurons];

	//Initialize Hidden Values Array
	hiddenValues = new double**[countHiddenLayers];
	for(int i=0; i<countHiddenLayers; i++)
	{
		double **arr;
		arr = new double*[countHiddenNeurons];
		for(int j=0; j<countHiddenNeurons; j++)
		{
			arr[j] = new double[3];
		}
		hiddenValues[i] = arr;
	}

	//Initialize Output Values Array
	outputValues = new double*[countOutputNeurons];
	for(int i=0; i<countOutputNeurons; i++)
		outputValues[i] = new double[3];


	///////////////////////////////////////////
	////Verify Input Neuron Array Allocation
	//cout << endl << "Input Neurons: " << endl;
	//for(int i=0; i<countInputNeurons; i++)
	//	inputValues[i] = i;

	//for(int i=0; i<countInputNeurons; i++)
	//	cout << inputValues[i] << ",";
	///////////////////////////////////////////

	///////////////////////////////////////////
	////Verify Hidden Neuron Array Allocation
	//cout << endl << "Hidden Neurons: " << endl;
	//for(int i=0; i<countHiddenLayers; i++)
	//	for(int j=0; j<countHiddenNeurons; j++)
	//		for(int k=0; k<3; k++)
	//			hiddenValues[i][j][k] = j;

	//for(int i=0; i<countHiddenLayers; i++)
	//{
	//	for(int j=0; j<countHiddenNeurons; j++)
	//	{
	//		for(int k=0; k<3; k++)
	//			cout << hiddenValues[i][j][k] << ",";

	//		cout << " - ";
	//	}
	//	cout <<"===";
	//}
	//////////////////////////////////////////

	//////////////////////////////////////////
	//// Verify Output Neuron Array Allocation
	//cout << endl << "Output Neurons: " << endl;
	//for(int i=0; i<countOutputNeurons; i++)
	//	for(int j=0; j<3; j++)
	//		outputValues[i][j] = i;

	//for(int i=0; i<countOutputNeurons; i++)
	//	for(int j=0; j<3; j++)
	//		cout << outputValues[i][j] << ",";
	///////////////////////////////////////////
	//
	///////////////////////////////////////////
	////Output Arrays Againt to Verify No Over-written records
	//cout << endl << "Input Neurons: " << endl;
	//for(int i=0; i<countInputNeurons; i++)
	//	cout << inputValues[i] << ",";


	//cout << endl << "Hidden Neurons: " << endl;
	//for(int i=0; i<countHiddenLayers; i++)
	//{
	//	for(int j=0; j<countHiddenNeurons; j++)
	//	{
	//		for(int k=0; k<3; k++)
	//			cout << hiddenValues[i][j][k] << ",";

	//		cout << " - ";
	//	}
	//	cout <<"===";
	//}

	//cout << endl << "Output Neurons: " << endl;
	//for(int i=0; i<countOutputNeurons; i++)
	//	for(int j=0; j<3; j++)
	//		cout << outputValues[i][j] << ",";
	///////////////////////////////////////////







	//Initialize Input-Hidden Weights Array
	ihWeights = new double*[countInputNeurons];
	for(int i=0; i<countInputNeurons; i++)
	{
		double *arr;
		arr = new double[countHiddenNeurons];
		ihWeights[i] = arr;
	}

	//Initialize Hidden-Hidden Weights Array
	if( countHiddenLayers > 1)
	{
		hhWeights = new double**[countHiddenLayers-1];
		for(int x=0; x<countHiddenLayers; x++)
		{
			double **a;
			a = new double*[countHiddenNeurons];
			for(int j=0; j<countHiddenNeurons; j++)
			{
				double *arr;
				arr = new double[countHiddenNeurons];
				
				a[j] = arr;
			}
			hhWeights[x] = a;
		}
	}


	//Initialize Hidden-Output Weights Array
	hoWeights = new double*[countHiddenNeurons];
	for(int i=0; i<countHiddenNeurons; i++)
	{
		double *arr;
		arr = new double[countOutputNeurons];
		for(int j=0; j<countOutputNeurons; j++)

		hoWeights[i] = arr;
	}
}//InitializeArrays()

void Backprop::RandomizeWeightAndBiasArrays()
{
	for(int i=0; i<countInputNeurons;i++)
		for(int j=0; j<countHiddenNeurons;j++)
			ihWeights[i][j] = RandomWeight(MaxWeightValue);

		for(int layer=0; layer<countHiddenLayers-1;layer++)
			for(int i=0; i<countHiddenNeurons;i++)
				for(int j=0; j<countHiddenNeurons;j++)
					hhWeights[layer][i][j] = RandomWeight(MaxWeightValue);

		for(int i=0; i<countHiddenNeurons;i++)
			for(int j=0; j<countOutputNeurons;j++)
				hoWeights[i][j] = RandomWeight(MaxWeightValue);

		//Randomize Bias Values
		for(int layer=0; layer<countHiddenLayers; layer++)
			for(int i=0; i<countHiddenNeurons; i++)
			{
				hiddenValues[layer][i][VALUE] = 0;
				hiddenValues[layer][i][BIAS] = RandomWeight(MaxBiasValue);
			}

		for(int i=0; i<countOutputNeurons; i++)
		{
			outputValues[i][VALUE] = 0;
			outputValues[i][BIAS] = RandomWeight(MaxBiasValue);
		}

}//RandomizeWeightAndBiasArrays()


void Backprop::ImportExemplarSet( void )
{
	string _METHODNAME = "ImportDataSet(filepath)";
	string line;
	vector<string> inputs;
	vector<string> sInputValues;
	vector<string> sOutputValues;
	countExemplarSets = numRowsInTextFile( DataSetFile );

	ifstream inStream;			//create new ifstream
	char buffer[16384];	//16384 should be enough for even a very large data row
	inStream.open(DataSetFile.c_str());

	inStream.getline(buffer,16384);	//Buffer
	line = buffer;
	Tokenize(line, inputs, "=");

	Tokenize(inputs[0], sInputValues, ",");
	Tokenize(inputs[1], sOutputValues,",");

	countInputNeurons = sInputValues.size();
	countOutputNeurons = sOutputValues.size();

	////Initialize Exemplar Arrays
	//Input Exemplars Array
	InputExemplars = new double*[countExemplarSets];
	for(int j=0; j<countExemplarSets; j++)
	{
		InputExemplars[j] = new double[countInputNeurons];
	}
	//OutputExemplars Array
	OutputExemplars = new double*[countExemplarSets];
	for(int j=0; j<countExemplarSets; j++)
	{
		OutputExemplars[j] = new double[countOutputNeurons];
	}

	for (int i=0; i<= countExemplarSets-1; i++)
	{
		vector<double> RowInputs(countInputNeurons,0);
		for (int k=0; k <= countInputNeurons-1; k++)
		{
			RowInputs[k] = atof(sInputValues[k].c_str());
		}

		vector<double> RowOutputs(countOutputNeurons,0);
		for (int k=0; k <= countOutputNeurons-1; k++)
		{
			RowOutputs[k] = atof(sOutputValues[k].c_str());
		}

		//Add the Row Vectors to the Exemplar Vectors
		for (int k=0; k<countInputNeurons; k++)
			InputExemplars[i][k] = RowInputs[k];
		
		for (int k=0; k<countOutputNeurons; k++)
			OutputExemplars[i][k] = RowOutputs[k];

		inStream.getline(buffer,16384);	//Buffer
		line = buffer;

		if (line != "")
		{
			inputs.clear();
			Tokenize(line, inputs, "=");

			vector<string> tempInput;
			vector<string> tempOutput;

			sInputValues = tempInput;
			sOutputValues = tempOutput;

			Tokenize(inputs[0], sInputValues, ",");
			Tokenize(inputs[1], sOutputValues,",");
		}
	}
	inStream.close();
}//ImportExemplarSet()

void Backprop::ImportBPROP2File( string filepath )
{
		ifstream inStream;			//create new ifstream
		string line;
		vector<string> tokens;
		char buffer[2048];

		try
		{
			inStream.open(filepath.c_str());

			inStream.getline(buffer,2048);	//Buffer
			line = buffer;

			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			DataSetFile = tokens[1];
			tokens.clear();

			inStream.getline(buffer,2048);	//Buffer
			inStream.getline(buffer,2048);	//Buffer
			
			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			EpochsPerInterval = atoi(tokens[1].c_str());
			tokens.clear();

			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			Intervals = atoi(tokens[1].c_str());
			tokens.clear();

			inStream.getline(buffer,2048);	//Buffer
			inStream.getline(buffer,2048);	//Buffer

			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			countHiddenNeurons = atoi(tokens[1].c_str());
			tokens.clear();

			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			countHiddenLayers = atoi(tokens[1].c_str());
			tokens.clear();

			inStream.getline(buffer,2048);	//Buffer
			inStream.getline(buffer,2048);	//Buffer

			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			MaxWeightValue = atof(tokens[1].c_str());
			tokens.clear();

			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			MaxBiasValue = atof(tokens[1].c_str());
			tokens.clear();

			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			Rho = atof(tokens[1].c_str());
			tokens.clear();

			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			RhoMultiplier = atof(tokens[1].c_str());
			tokens.clear();

			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			RhoInterval = atoi(tokens[1].c_str());
			tokens.clear();

			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			if(tokens[1] == "True" || tokens[1] == "T")
				Momentum = true;
			else
				Momentum = false;

			tokens.clear();

			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			MomentumScaler = atof(tokens[1].c_str());
			tokens.clear();

			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			SimAnnealMaxPercent = atoi(tokens[1].c_str());
			tokens.clear();

			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			SimAnnealInterval = atoi(tokens[1].c_str());
			tokens.clear();

			inStream.getline(buffer,2048);	//Buffer
			inStream.getline(buffer,2048);	//Buffer

			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			SaveInterval = atoi(tokens[1].c_str());
			tokens.clear();

			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			FileTitle = tokens[1];
			tokens.clear();

			inStream.getline(buffer,2048);	//Buffer
			inStream.getline(buffer,2048);	//Buffer

			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			ConsoleLoggingInterval = atoi(tokens[1].c_str());
			tokens.clear();

		}
		catch(...)
		{
			cout << "Error Reading BPROP2 File!";
		}

		inStream.close();

}//ImportBPROP2File

void Backprop::ImportBPROPFile( string filepath )
{
		ifstream inStream;			//create new ifstream
		string line;
		vector<string> tokens;
		char buffer[2048];

		try
		{
			inStream.open(filepath.c_str());

			inStream.getline(buffer,2048);	//Buffer
			line = buffer;

			inStream.getline(buffer,2048);	//Ignore ID
			line = buffer;
			
			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			FileTitle = tokens[1];
			tokens.clear();

			inStream.getline(buffer,2048);	//Buffer
			inStream.getline(buffer,2048);	//Buffer

			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			DataSetFile = tokens[1];
			tokens.clear();

			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			ANNFileName = tokens[1];
			tokens.clear();

			inStream.getline(buffer,2048);	//Buffer
			inStream.getline(buffer,2048);	//Buffer

			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			Rho = atof(tokens[1].c_str());
			tokens.clear();

			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			RhoMultiplier = atof(tokens[1].c_str());
			tokens.clear();

			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			RhoInterval = atoi(tokens[1].c_str());
			tokens.clear();

			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			if(tokens[1] == "True" || tokens[1] == "T")
				Momentum = true;
			else
				Momentum = false;

			tokens.clear();

			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			MomentumScaler = atof(tokens[1].c_str());
			tokens.clear();

			inStream.getline(buffer,2048);
			line = buffer;
			Tokenize(line, tokens, "=");
			SimAnnealMaxPercent = atoi(tokens[1].c_str());
			tokens.clear();

			inStream.getline(buffer,2048);
			line = buffer;
			if( line != "" )
			{
			Tokenize(line, tokens, "=");
			SimAnnealInterval = atoi(tokens[1].c_str());
			tokens.clear();
			}
			else
			{
				cout << "Warning- BPROP File Lacking SimAnnealValue (Probably using obsolete BPROP version) - using default value: 25" << endl;
				SimAnnealInterval = 25;
			}
		}
		catch(...)
		{
			cout << "Error Reading BPROP File!";
		}

		inStream.close();

}//ImportBPROPFile


void Backprop::RandomizeTheIndex( void )
{
	//Set initial values
	for(int i=0; i<countExemplarSets; i++)
		RandomArray[i] = i;

	//Randomize the values
	int rand1, rand2, tempVal;
	for(int i=0; i<countExemplarSets; i++)
	{
		rand1 = AbsVal(RandomNumber(countExemplarSets));
		rand2 = AbsVal(RandomNumber(countExemplarSets));

		tempVal = RandomArray[rand1];
		RandomArray[rand1] = RandomArray[rand2];
		RandomArray[rand2] = tempVal;
	}
}

void Backprop::Tokenize(const string& str, vector<string>& tokens, const string& delimiters = " ")
{
	// Skip delimiters at beginning.
	string::size_type lastPos = str.find_first_not_of(delimiters, 0);
	// Find first "non-delimiter".
	string::size_type pos     = str.find_first_of(delimiters, lastPos);

	while (string::npos != pos || string::npos != lastPos)
	{
		// Found a token, add it to the vector.
		tokens.push_back(str.substr(lastPos, pos - lastPos));
		// Skip delimiters.  Note the "not_of"
		lastPos = str.find_first_not_of(delimiters, pos);
		// Find next "non-delimiter"
		pos = str.find_first_of(delimiters, lastPos);
	}
}//Tokenize

int Backprop::numRowsInTextFile( string filepath )
{
	ifstream inStream;			//create new ifstream
	string line;
	int rows = 0;
	char buffer[16384];	//16384 should be enough for even a very large data row

	inStream.open(filepath.c_str());		

	do
	{
		inStream.getline(buffer,16384);	//Buffer
		line = buffer;
		if( line != "" )
			rows += 1;
	}
	while(line != "");

	return rows;

}

double Backprop::RandomWeight( double maxValue )
{
	double randWeight;
	if( maxValue > 1 || maxValue < -1)
		randWeight = RandomNumber(maxValue-1) + (double(RandomNumber(1000)) / 1000);
	else
		randWeight = (double(RandomNumber(1000)) / 1000) * maxValue + (double(RandomNumber(1000000)) / 1000000) * maxValue ;

	return randWeight;
}//RandomWeight()

int Backprop::RandomNumber( int MaxNumber )
{
	int randNum;
	int SignQualifier;

	randNum = rand() % MaxNumber;
	SignQualifier = rand() % 100;

	if( SignQualifier > 50 )
		return randNum;
	else
		return -randNum;
}

double Backprop::AbsVal( double value )
{
	if( value < 0 )
		return -value;
	else
		return value;
}//AbsVal()

double Backprop::CalcFitness( void )
{
	double Fitness = 0;
	double ActualValue;
	double DesiredValue;

	for(int iteration=0; iteration<countExemplarSets; iteration++)
	{
		
		////////////////////////////////////////////////////////////////////////////
		//Run The Network
		////////////////////////////////////////////////////////////////////////////

		//Set Input Neurons
		for(int i=0; i<=countInputNeurons-1; i++)
			inputValues[i] = InputExemplars[iteration][i];

		//Calculate First Layer of Hidden Neuron Values
		for(int hiddenNeuron=0; hiddenNeuron<countHiddenNeurons; hiddenNeuron++)
		{
			hiddenValues[0][hiddenNeuron][VALUE] = 0; //Reset to 0
			for(int inputNeuron=0; inputNeuron<countInputNeurons; inputNeuron++)
				hiddenValues[0][hiddenNeuron][VALUE] += inputValues[inputNeuron] * ihWeights[inputNeuron][hiddenNeuron];

			//hiddenValues[0][hiddenNeuron][VALUE] = Activation(hiddenValues[0][hiddenNeuron][VALUE] + hiddenValues[0][hiddenNeuron][BIAS]);
			hiddenValues[0][hiddenNeuron][VALUE] = (1 / (1 + pow(2.71828183,-(hiddenValues[0][hiddenNeuron][VALUE] + hiddenValues[0][hiddenNeuron][BIAS]))));
		}

		//Calculate Hidden Layer Neuron Values
		if( countHiddenLayers > 1)
		{
			for(int layer=1; layer<countHiddenLayers; layer++)
			{
				for(int aboveHidden=0; aboveHidden<countHiddenNeurons; aboveHidden++)
				{
					hiddenValues[layer][aboveHidden][VALUE] = 0;
					for(int belowHidden=0; belowHidden<countHiddenNeurons; belowHidden++)
					{
						hiddenValues[layer][aboveHidden][VALUE] += hiddenValues[layer-1][belowHidden][VALUE] * hhWeights[layer-1][belowHidden][aboveHidden];
					}
					//hiddenValues[layer][aboveHidden][VALUE] = Activation(hiddenValues[layer][aboveHidden][VALUE] +hiddenValues[layer][aboveHidden][BIAS]);
					hiddenValues[layer][aboveHidden][VALUE] = (1 / (1 + pow(2.71828183,-(hiddenValues[layer][aboveHidden][VALUE] + hiddenValues[layer][aboveHidden][BIAS]))));
				}
			}
		}

		//Calculate Output Neuron Values
		for( int outputNeuron=0; outputNeuron<countOutputNeurons; outputNeuron++)
		{
			outputValues[outputNeuron][VALUE] = 0;	//Reset to 0
			for(int hiddenNeuron=0; hiddenNeuron<countHiddenNeurons; hiddenNeuron++)
			{
				outputValues[outputNeuron][VALUE] += hiddenValues[countHiddenLayers-1][hiddenNeuron][VALUE] * hoWeights[hiddenNeuron][outputNeuron];
			}
			//We are not squashing the output neurons, so simply add the bias to it
			outputValues[outputNeuron][VALUE] += outputValues[outputNeuron][BIAS];
		}

			/////////////////////////////////////////////////////////////////////////
			// END RUN NETWORK													   //
			/////////////////////////////////////////////////////////////////////////

		for(int i=0; i<=countOutputNeurons-1; i++)
		{
			ActualValue = outputValues[i][VALUE];
			DesiredValue = OutputExemplars[iteration][i];
			Fitness += AbsVal(DesiredValue - ActualValue);
		}
	}
	return Fitness;
}//calcFitness()

void Backprop::OutputBPROP2File( string filename)
{
	if( DataSetFile == "")
		throw exception("Error creating BPROP2 File: DataFileName cannot be nothing!");

	if( ANNFileName == "")
		throw exception("Error creating BPROP2 File: ANNFileName cannot be nothing, output ANN File first, then create BPROP file.");

	ofstream file;
	file.open(filename.c_str());
	file.precision(LargeSetPrecisionValue);
	file.setf(ios::fixed);
	try
	{
            file << "### Data Set ###" << endl;
            file << "Dataset File Location=" << DataSetFile << endl;
            file << endl;
            file << "### Backprop Configuration ###" << endl;
            file << "Epochs Per Interval=" << EpochsPerInterval << endl;
            file << "Intervals=" << Intervals << endl;
            file << endl;
			file << "### Network Topology ###" << endl;
			file << "Hidden Neurons=" << countHiddenNeurons << endl;
			file << "Hidden Layers=" << countHiddenLayers << endl;
			file << endl;
            file << "### Training Configuration ###" << endl;
			file << "Max Weight Value=" << MaxWeightValue << endl;
			file << "Max Bias Value=" << MaxBiasValue << endl;
            file << "Initial rho value=" << Rho << endl;
            file << "Rho multiplier=" << RhoMultiplier << endl;
            file << "Rho interval=" << RhoInterval << endl;
            file << "Use Momentum=" << Momentum << endl;
            file << "Momentum scaler=" << MomentumScaler << endl;
            file << "Simulated annealing max percent(%)=" << SimAnnealMaxPercent << endl;
			file << "Simulated annealing interval=" << SimAnnealInterval << endl;
			file << endl;
			file << "### Save Data Settings ###" << endl;
			file << "Save Interval=" << SaveInterval << endl;
			file << "File Title=" << FileTitle << endl;
			file << endl;
			file << "### Console Logging ###" << endl;
			file << "Console Logging Interval=" << ConsoleLoggingInterval << endl;
	}
	catch(...)
	{
		cout << "Exception Occurred!";
	}
	file.close();
}//outputBPROP2File()

void Backprop::OutputBPROPFile( string filename)
{

	if( DataSetFile == "")
		throw exception("Error creating BPROP File: DataFileName cannot be nothing!");

	if( ANNFileName == "")
		throw exception("Error creating BPROP File: ANNFileName cannot be nothing, output ANN File first, then create BPROP file.");

	ofstream file;
	file.open(filename.c_str());
	file.precision(LargeSetPrecisionValue);
	file.setf(ios::fixed);
	try
	{
            file << "### Backprop Identification ###" << endl;
            file << "ID=" << FileTitle << endl;
            file << "File Title=" << FileTitle << endl;
            file << endl;
            file << "### Input Files - !!! NEVER MODIFY THIS PART !!! ###" << endl;
            file << "DO NOT MODIFY: Dataset file location=" << DataSetFile << endl;
            file << "DO NOT MODIFY: ANN file location=" << ANNFileName << endl;
            file << endl;
            file << "### Training Configuration ###" << endl;
            file << "Initial rho value=" << Rho << endl;
            file << "Rho multiplier=" << RhoMultiplier << endl;
            file << "Rho interval=" << RhoInterval << endl;
            file << "Use Momentum=" << Momentum << endl;
            file << "Momentum scaler=" << MomentumScaler << endl;
            file << "Simulated annealing max percent(%)=" << SimAnnealMaxPercent << endl;
			file << "Simulated annealing interval=" << SimAnnealInterval << endl;

	}
	catch(...)
	{
		cout << "Exception Occurred!";
	}
	file.close();
}

void Backprop::OutputNetwork( string fileName )
{
	ANNFileName = fileName;		//Save ANN File Path for use in BPROP File output

	ofstream file;
	file.open(fileName.c_str());
	file.precision(LargeSetPrecisionValue);
	file.setf(ios::fixed);
	file << countInputNeurons << endl;
	file << countHiddenNeurons << endl;
	file << countHiddenLayers << endl;
	file << countOutputNeurons << endl;
	file << ((countInputNeurons + 1) * countHiddenNeurons) + ((countHiddenLayers - 1) * (countHiddenNeurons + 1) * countHiddenNeurons) + ((countHiddenNeurons + 1) * countOutputNeurons) << endl;

	for( int hiddenNeuron=0; hiddenNeuron<=countHiddenNeurons-1; hiddenNeuron++)
	{
		for( int inputNeuron=0; inputNeuron<=countInputNeurons-1; inputNeuron++)
			file << ihWeights[inputNeuron][hiddenNeuron] <<endl;

		file << hiddenValues[0][hiddenNeuron][BIAS] << endl;
	}

	if( countHiddenLayers > 1)
	{
		for( int layer=0; layer<=countHiddenLayers-2; layer++)
		{
			for( int HiddenAbove=0; HiddenAbove<=countHiddenNeurons-1; HiddenAbove++)
			{
				for( int HiddenBelow=0; HiddenBelow<=countHiddenNeurons-1; HiddenBelow++)
					file << hhWeights[layer][HiddenBelow][HiddenAbove] << endl;

				file << hiddenValues[layer+1][HiddenAbove][BIAS] << endl;
			}
		}
	}

	for( int outputNeuron=0; outputNeuron<=countOutputNeurons-1; outputNeuron++)
	{
		for( int hiddenNeuron=0; hiddenNeuron<=countHiddenNeurons-1; hiddenNeuron++)
			file << hoWeights[hiddenNeuron][outputNeuron] << endl;

		file << outputValues[outputNeuron][BIAS] << endl;
	}

	file.close();
}//outputNetwork()

void Backprop::OutputNetworkOutput( string fileName )
{
	double Fitness = CalcFitness();
	ofstream file;
	file.open(fileName.c_str());
	file.precision(SmallSetPrecisionValue);
	file.setf(ios::fixed);
	try
	{
		file << "Fitness: " << Fitness << " Avg: " << (Fitness / countExemplarSets) << " Rho: " << Rho << endl;

		for( int i=0; i <=countExemplarSets-1; i++ )
		{
			for( int j=0; j<=countInputNeurons-1; j++ )
			{
				file << InputExemplars[i][j];
				if( j != countInputNeurons-1)
					file << ",";
			}

			//////////////////////////////////////////////////////////////////////////
			// BEGIN RUN NETWORK													//
			//////////////////////////////////////////////////////////////////////////

			//Set Input Neurons
			for(int j=0; j<=countInputNeurons-1; j++)
				inputValues[j] = InputExemplars[i][j];

			//Calculate First Layer of Hidden Neuron Values
			for(int hiddenNeuron=0; hiddenNeuron<countHiddenNeurons; hiddenNeuron++)
			{
				hiddenValues[0][hiddenNeuron][VALUE] = 0; //Reset to 0
				for(int inputNeuron=0; inputNeuron<countInputNeurons; inputNeuron++)
					hiddenValues[0][hiddenNeuron][VALUE] += inputValues[inputNeuron] * ihWeights[inputNeuron][hiddenNeuron];

				//hiddenValues[0][hiddenNeuron][VALUE] = Activation(hiddenValues[0][hiddenNeuron][VALUE] + hiddenValues[0][hiddenNeuron][BIAS]);
				hiddenValues[0][hiddenNeuron][VALUE] = (1 / (1 + pow(2.71828183,-(hiddenValues[0][hiddenNeuron][VALUE] + hiddenValues[0][hiddenNeuron][BIAS]))));
			}

			//Calculate Hidden Layer Neuron Values
			if( countHiddenLayers > 1)
			{
				for(int layer=1; layer<countHiddenLayers; layer++)
				{
					for(int aboveHidden=0; aboveHidden<countHiddenNeurons; aboveHidden++)
					{
						hiddenValues[layer][aboveHidden][VALUE] = 0;
						for(int belowHidden=0; belowHidden<countHiddenNeurons; belowHidden++)
						{
							hiddenValues[layer][aboveHidden][VALUE] += hiddenValues[layer-1][belowHidden][VALUE] * hhWeights[layer-1][belowHidden][aboveHidden];
						}
						//hiddenValues[layer][aboveHidden][VALUE] = Activation(hiddenValues[layer][aboveHidden][VALUE] +hiddenValues[layer][aboveHidden][BIAS]);
						hiddenValues[layer][aboveHidden][VALUE] = (1 / (1 + pow(2.71828183,-(hiddenValues[layer][aboveHidden][VALUE] + hiddenValues[layer][aboveHidden][BIAS]))));
					}
				}
			}

			//Calculate Output Neuron Values
			for( int outputNeuron=0; outputNeuron<countOutputNeurons; outputNeuron++)
			{
				outputValues[outputNeuron][VALUE] = 0;	//Reset to 0
				for(int hiddenNeuron=0; hiddenNeuron<countHiddenNeurons; hiddenNeuron++)
				{
					outputValues[outputNeuron][VALUE] += hiddenValues[countHiddenLayers-1][hiddenNeuron][VALUE] * hoWeights[hiddenNeuron][outputNeuron];
				}
				//We are not squashing the output neurons, so simply add the bias to it
				outputValues[outputNeuron][VALUE] += outputValues[outputNeuron][BIAS];
			}

			/////////////////////////////////////////////////////////////////////////
			// END RUN NETWORK													   //
			/////////////////////////////////////////////////////////////////////////

			file << "=";

			for( int j=0; j<=countOutputNeurons-1; j++ )
			{
				file << outputValues[j][VALUE];
				if( j != countOutputNeurons-1 )
					file << ",";
			}
			file << endl;
		}
	}
	catch(...)
	{
		cout << "Exception Occurred";
	}
	file.close();
}//outputNetworkOutput()

void Backprop::SaveData( void )
{
	SetConsoleTextAttribute(hConsole, White);	//Color Console
	string SaveName = GenerateSaveName();
	cout << "Saving data to: " << CurrentDirectory << SaveName << "\\" << endl;
	try
	{
		string command;
		command = "mkdir \"" + CurrentDirectory + SaveName + "\"";
		system(command.c_str());

		//cout <<"Saving Network... " << endl;
		OutputNetwork(CurrentDirectory + SaveName + "\\" + SaveName + ".ANN");
		//cout <<"Saving Network Output... " << endl;
		OutputNetworkOutput(CurrentDirectory + SaveName + "\\" + SaveName + ".txt");
		//cout <<"Saving BPROP2 File... " << endl;
		OutputBPROP2File(CurrentDirectory + SaveName + "\\" + SaveName + ".BPROP2");
		//cout <<"Saving BPROP File... " << endl;
		OutputBPROPFile(CurrentDirectory + SaveName + "\\" + SaveName + ".BPROP");
	}
	catch(...)
	{
		cout << "Exception Occurred";
	}
	cout << "Data Saved Successfully!" << endl;

}

void Backprop::DeconstructCurrentDirectory( string filepath )
{
	vector<string> tokens;

	Tokenize(filepath, tokens, "\\");

	if( tokens.size() > 1) 
	{
		for( int i = 0; i <= tokens.size() -2; i++)
		{
			if( i == 0 )
				CurrentDirectory = tokens[i] + "\\";
			else
				CurrentDirectory += tokens[i] + "\\";
		}
	}
	else
		CurrentDirectory = "";
}

string Backprop::GenerateSaveName( void )
{
	string AvgError;
	string Dt;

	//Create Average Error String
	ostringstream oss;
	oss << (CalcFitness() / countExemplarSets);
	AvgError = oss.str();

//      'Create Date String
//      Dim CurrentDate As Date = Now
//      Dt = CurrentDate.Month.ToString.PadLeft(2, "0"c) & CurrentDate.Day.ToString.PadLeft(2, "0"c) & CurrentDate.Year.ToString.Substring(2)
//### Add Date Part Later?

	string output;
	output = AvgError;
	output.append("_");
	output.append(FileTitle);

	return output;
}

//void Backprop::SelectInputFile( void )
//{
//	//Populate AppPreviousEntries
//	countPreviousEntries = 0;
//	ifstream in("AppPreviousEntries.txt"); // input
//
//	if(!in) {
//	cout << "Cannot open or find AppPreviousEntries.txt file.\n";
//	}
//	char buffer[256];
//
//	for(int i=0; i<10; i++)
//	{
//		in.getline(buffer,256);
//		AppPreviousEntries[i] = buffer;
//		if(AppPreviousEntries[i] != "")
//			countPreviousEntries += 1;
//	}
//	in.close();
//
//
//	//Begin Input File Selection
//	string file;
//	cout << "Previous Input Files: " << endl;
//	for(int i=0; i<10; i++)
//	{
//		if(AppPreviousEntries[i] != "")
//			cout << "[" << i+1 << "] - " << AppPreviousEntries[i] << endl;
//	}
//InputFile:
//	cout << "Please Enter Number of Previous Entry to Use or New File Location: ";
//	
//	string input;
//	cin >> input;
//
//	//Check If Input File Exists
//	////Admittably this is not the best method of checking if a file exists, but whatever.
//	if( atoi(input.c_str()) > countPreviousEntries || atoi(input.c_str()) < 1 )
//	{
//		ifstream testFile(input.c_str());
//		if( !testFile )
//		{
//			cout << "File Does Not Exist! Please Try Again." << endl;
//			goto InputFile;
//		}
//	}
//
//	
//	int nStartBPROP2, nStartBPROP;
//	nStartBPROP2 = input.find ( string ( ".BPROP2"));
//	nStartBPROP = input.find ( string ( ".BPROP"));
//
//	if(atoi(input.c_str()) <= countPreviousEntries && atoi(input.c_str()) > 0)
//		input = AppPreviousEntries[atoi(input.c_str())-1];
//	else if( nStartBPROP != -1 || nStartBPROP2 != -1 )
//	{
//		//AddFileToPreviousEntries(input);
//		for(int i=countPreviousEntries-1; i>=0; i--)
//		if(i != 9 )
//			AppPreviousEntries[i+1] = AppPreviousEntries[i];
//
//		AppPreviousEntries[0] = input;
//
//		//Update Previous Entries File
//		ofstream ofile;
//		ofile.open("AppPreviousEntries.txt");
//
//		for(int i=0; i<10; i++)
//		{
//			if( AppPreviousEntries[i] != "" )
//				ofile << AppPreviousEntries[i] << endl;
//		}
//		ofile.close();
//	}
//	else
//	{
//		cout << "Invalid Input Entered! Please Re-enter" << endl;
//		goto InputFile;
//	}
//	cout << "Loading: " << input << "..." << endl;
//	InputFile = input;	
//
//}//SelectInputFile
//
//void Backprop::LoadBPROP2File( void )
//{
//	int RandSeed = time(NULL)-clock();
//	srand ( RandSeed );
//	//srand(1);
//
//	cout << "Importing BPROP2 File...";
//	DeconstructCurrentDirectory(InputFile);
//	ImportBPROP2File(InputFile);
//	cout << "		Success!" << endl;
//
//	cout <<"Importing DataSet File...";
//	ImportExemplarSet();
//	cout << "		Success!" << endl;
//
//	cout << "Initializing Arrays...";
//	InitializeArrays();
//	cout << "			Success!" << endl;
//
//	cout << "Randomizing Arrays("<< RandSeed << ")...";
//	RandomizeWeightAndBiasArrays();
//	cout << "	Success!" << endl;
//	cout << endl;
//}
//
//void Backprop::LoadBPROPFile( void )
//{
//	cout << "Importing BPROP File...";
//	DeconstructCurrentDirectory(InputFile);
//	ImportBPROPFile(InputFile);
//	cout << "		Success!" << endl;
//
//	cout <<"Importing DataSet File...";
//	ImportExemplarSet();
//	cout << "		Success!" << endl;
//
//	//Arrays will be initialized while importing the ANN file
//	////The arrays cannot be initalized any sooner because we wont know the full architecture until we look at the ANN file
//	cout << "Importing ANN File...";
//	ImportANNFile();
//	cout << " Success!(ANN)" << endl;
//	cout << endl;
//
//
//	//OUTPUTTING WEIGHTS
//	cout << endl << "howeights:" << endl;
//			for(int hiddenNeuron=0; hiddenNeuron<countHiddenNeurons; hiddenNeuron++)
//				for(int outputNeuron=0; outputNeuron<countOutputNeurons; outputNeuron++)
//						cout << hoWeights[hiddenNeuron][outputNeuron] << ",";
//
//	cout << endl << "hhweights:" << endl;
//				//Calculate Hidden-Hidden Sim Annealed Weights
//				if( countHiddenLayers>1)
//					for(int layer=countHiddenLayers-2; layer>=0; layer--)
//						for(int belowHidden=0; belowHidden<countHiddenNeurons; belowHidden++)
//							for(int aboveHidden=0; aboveHidden<countHiddenNeurons; aboveHidden++)
//								cout << hhWeights[layer][belowHidden][aboveHidden] << ",";
//
//	cout << endl << "ihweights:" << endl;
//				//Calculate Input-Hidden Sim Annealed Weights
//				for(int inputNeuron=0; inputNeuron<countInputNeurons; inputNeuron++)
//					for(int hiddenNeuron=0; hiddenNeuron<countHiddenNeurons; hiddenNeuron++)
//						cout << ihWeights[inputNeuron][hiddenNeuron] << ",";
//
//	cout << endl;
//	cout << "Epochs Per Interval: ";
//	cin >> EpochsPerInterval;
//	cout << "Intervals: ";
//	cin >> Intervals;
//	cout << "Save Interval: ";
//	cin >> SaveInterval;
//	cout << "Console Logging Epoch Interval: ";
//	cin >> ConsoleLoggingInterval;
//	cout << endl;
//
//}

void Backprop::ImportANNFile( void )
{
	if( ANNFileName == "")
	{	cout << "Error - No ANN Input File is known. Exiting...";	exit;	}

	ifstream inStream;			//create new ifstream
	string line;
	char buffer[2048];

	try
	{
		inStream.open(ANNFileName.c_str());

		//Read in countInputNeurons and verify
		inStream.getline(buffer,2048);	//Buffer
		line = buffer;
		if( atoi(line.c_str()) != countInputNeurons )
		{	cout << "Error - Invalid ANN File. Input Neuron Count does not match! Exiting...";	exit;	}

		//Read in countHiddenNeurons and verify
		inStream.getline(buffer,2048);	//Buffer
		line = buffer;
		countHiddenNeurons = atoi(line.c_str());

		//Read in countHiddenLayers and verify
		inStream.getline(buffer,2048);	//Buffer
		line = buffer;
		countHiddenLayers = atoi(line.c_str());

		//Read in countOutputNeurons and verify
		inStream.getline(buffer,2048);	//Buffer
		line = buffer;
		if( atoi(line.c_str()) != countOutputNeurons )
		{	cout << "Error - Invalid ANN File. Output Neuron Count does not match! Exiting...";	exit;	}

		//Read in count Total Weights (not used in this program, but used in others)
		inStream.getline(buffer,2048);

		//We now know the full network architecture, now we can initalize the arrays...
		cout << " Initializing Arrays...";
		InitializeArrays();
		cout << " Success! (Arrays)";


	//	'Read in the Input-Hidden weights and Biases
	//	For hiddenNeuron As Integer = 0 To countHiddenNeurons - 1
	//		For inputNeuron As Integer = 0 To countInputNeurons - 1
	//			ihWeights(inputNeuron, hiddenNeuron, CURRENT) = Convert.ToDouble(sr.ReadLine)
	//		Next
	//		hiddenValues(hiddenNeuron, 0, BIAS) = Convert.ToDouble(sr.ReadLine)
	//	Next
	//	'Below is the code used to write out the I-H Weights
	//	'For hiddenNeuron As Integer = 0 To countHiddenNeurons - 1
	//	'    For inputNeuron As Integer = 0 To countInputNeurons - 1
	//	'        sw.WriteLine(ihWeights(inputNeuron, hiddenNeuron, CURRENT))
	//	'    Next
	//	'    sw.WriteLine(hiddenValues(hiddenNeuron, 0, BIAS))
	//	'Next
		//Read in the Input-Hidden Weights and Biases
		for( int hiddenNeuron=0; hiddenNeuron < countHiddenNeurons; hiddenNeuron++ )
		{
			for( int inputNeuron=0; inputNeuron < countInputNeurons; inputNeuron++ )
			{
				inStream.getline(buffer,2048);	//Buffer
				line = buffer;
				ihWeights[inputNeuron][hiddenNeuron] = atof(line.c_str());
			}//for
			inStream.getline(buffer,2048);	//Buffer
			line = buffer;
			hiddenValues[0][hiddenNeuron][BIAS] = atof(line.c_str());
		}//for


	//	If countHiddenLayers > 1 Then
	//		For layer As Integer = 0 To countHiddenLayers - 2
	//			For HiddenAbove As Integer = 0 To countHiddenNeurons - 1
	//				For HiddenBelow As Integer = 0 To countHiddenNeurons - 1
	//					hhWeights(HiddenBelow, HiddenAbove, layer, CURRENT) = Convert.ToDouble(sr.ReadLine)
	//				Next
	//				hiddenValues(HiddenAbove, layer + 1, BIAS) = Convert.ToDouble(sr.ReadLine)
	//			Next
	//		Next
	//	End If
	//	'Below is the code used to write the H-H weights and biases
	//	'If countHiddenLayers > 1 Then
	//	'    For layer As Integer = 0 To countHiddenLayers - 2
	//	'        For HiddenAbove As Integer = 0 To countHiddenNeurons - 1
	//	'            For HiddenBelow As Integer = 0 To countHiddenNeurons - 1
	//	'                sw.WriteLine(hhWeights(HiddenBelow, HiddenAbove, layer, CURRENT))
	//	'            Next
	//	'            sw.WriteLine(hiddenValues(HiddenAbove, layer + 1, BIAS))
	//	'        Next
	//	'    Next
	//	'End If
		//Read in the Hidden-Hidden Weights and Bias
		if( countHiddenLayers > 1)
		{
			for( int layer=0; layer < countHiddenLayers-1; layer++ )
			{
				for( int hiddenAbove=0; hiddenAbove<countHiddenNeurons; hiddenAbove++ )
				{
					for( int hiddenBelow=0; hiddenBelow<countHiddenNeurons; hiddenBelow++ )
					{
						inStream.getline(buffer,2048);	//Buffer
						line = buffer;
						hhWeights[layer][hiddenBelow][hiddenAbove] = atof(line.c_str());
					}//for
					inStream.getline(buffer,2048);	//Buffer
					line = buffer;
					hiddenValues[layer+1][hiddenAbove][BIAS] = atof(line.c_str());
				}//for
			}//for
		}//if



	//	For outputNeuron As Integer = 0 To countOutputNeurons - 1
	//		For hiddenNeuron As Integer = 0 To countHiddenNeurons - 1
	//			hoWeights(hiddenNeuron, outputNeuron, CURRENT) = Convert.ToDouble(sr.ReadLine)
	//		Next
	//		outputValues(outputNeuron, BIAS) = Convert.ToDouble(sr.ReadLine)
	//	Next
	//	'Below is the code used to write out the H-O weights and biases
	//	'For outputNeuron As Integer = 0 To countOutputNeurons - 1
	//	'    For hiddenNeuron As Integer = 0 To countHiddenNeurons - 1
	//	'        sw.WriteLine(hoWeights(hiddenNeuron, outputNeuron, CURRENT))
	//	'    Next
	//	'    sw.WriteLine(outputValues(outputNeuron, BIAS))
	//	'Next
		for( int outputNeuron=0; outputNeuron<countOutputNeurons; outputNeuron++ )
		{
			for( int hiddenNeuron=0; hiddenNeuron<countHiddenNeurons; hiddenNeuron++ )
			{
				inStream.getline(buffer,2048);	//Buffer
				line = buffer;
				hoWeights[hiddenNeuron][outputNeuron] = atof(line.c_str());
			}//for
			inStream.getline(buffer,2048);	//Buffer
			line = buffer;
			outputValues[outputNeuron][BIAS] = atof(line.c_str());
		}//for

	}
	catch(...)
	{
		cout << "Error Reading ANN File!";
	}

	inStream.close();

}

void Backprop::OutputWeightsToConsole( void )
{
	//Calculate Input-Hidden Sim Annealed Weights
	for(int inputNeuron=0; inputNeuron<countInputNeurons; inputNeuron++)
		for(int hiddenNeuron=0; hiddenNeuron<countHiddenNeurons; hiddenNeuron++)
			cout << "ihWeights[" << inputNeuron << "][" << hiddenNeuron << "]=" << ihWeights[inputNeuron][hiddenNeuron] << endl;

	cout << endl;

	//Calculate Hidden-Hidden Sim Annealed Weights
	if( countHiddenLayers>1)
		for(int layer=countHiddenLayers-2; layer>=0; layer--)
			for(int belowHidden=0; belowHidden<countHiddenNeurons; belowHidden++)
				for(int aboveHidden=0; aboveHidden<countHiddenNeurons; aboveHidden++)
					cout << "hhWeights[" << layer << "][" << belowHidden << "][" << aboveHidden << "]=" << hhWeights[layer][belowHidden][aboveHidden] << endl;

	cout << endl;

	for(int hiddenNeuron=0; hiddenNeuron<countHiddenNeurons; hiddenNeuron++)
		for(int outputNeuron=0; outputNeuron<countOutputNeurons; outputNeuron++)
				cout << "hoWeights[" << hiddenNeuron << "][" << outputNeuron << "]=" << hoWeights[hiddenNeuron][outputNeuron] << endl;

}//OutputWeightsToConsole

void Backprop::OutputNeuronsToConsole( void )
{

	//Set Input Neurons
	for(int i=0; i<=countInputNeurons-1; i++)
		cout << "inputValues[" << i << "]=" << inputValues[i] << endl;

	//Calculate First Layer of Hidden Neuron Values
	for(int hiddenNeuron=0; hiddenNeuron<countHiddenNeurons; hiddenNeuron++)
	{
		for(int inputNeuron=0; inputNeuron<countInputNeurons; inputNeuron++)
			cout << "hiddenValues[0][" << hiddenNeuron << "][" << VALUE << "]=" << hiddenValues[0][hiddenNeuron][VALUE] << endl;

		//hiddenValues[0][hiddenNeuron][VALUE] = Activation(hiddenValues[0][hiddenNeuron][VALUE] + hiddenValues[0][hiddenNeuron][BIAS]);
		cout << "hiddenValues[0][" << hiddenNeuron << "][" << BIAS << "]=" << hiddenValues[0][hiddenNeuron][BIAS] << endl;
		cout << "hiddenValues[0][" << hiddenNeuron << "][" << DELTA << "]=" << hiddenValues[0][hiddenNeuron][DELTA] << endl;
	}

	//Calculate Hidden Layer Neuron Values
	if( countHiddenLayers > 1)
	{
		for(int layer=1; layer<countHiddenLayers; layer++)
		{
			for(int aboveHidden=0; aboveHidden<countHiddenNeurons; aboveHidden++)
			{
				for(int belowHidden=0; belowHidden<countHiddenNeurons; belowHidden++)
				{
					cout << "hiddenValues[" << layer << "][" << aboveHidden << "][" << VALUE << "]=" << hiddenValues[layer][aboveHidden][VALUE] << endl;
				}
				//hiddenValues[layer][aboveHidden][VALUE] = Activation(hiddenValues[layer][aboveHidden][VALUE] +hiddenValues[layer][aboveHidden][BIAS]);
				cout << "hiddenValues[" << layer << "][" << aboveHidden << "][" << BIAS << "]=" << hiddenValues[layer][aboveHidden][BIAS] << endl;
			}
		}
	}

	//Calculate Output Neuron Values
	for( int outputNeuron=0; outputNeuron<countOutputNeurons; outputNeuron++)
	{
		cout << "outputValues[" << outputNeuron << "][" << VALUE << "]=" << outputValues[outputNeuron][VALUE] << endl;
		//We are not squashing the output neurons, so simply add the bias to it
		cout << "outputValues[" << outputNeuron << "][" << BIAS << "]=" << outputValues[outputNeuron][BIAS] << endl;
		cout << "outputValues[" << outputNeuron << "][" << DELTA << "]=" << outputValues[outputNeuron][DELTA] << endl;
	}
}//OutputNeuronsToConsole

int Backprop::RuntimeConfigUpdate( void )
{
	SetConsoleTextAttribute(hConsole, White);	//Color Console
	int SavedData = 0;
	bool ResetConfigFile = false;

	ifstream inStream;			//create new ifstream
	string line;
	vector<string> tokens;
	char buffer[2048];

	try
	{
		inStream.open("RuntimeUpdates.config");

		if(!inStream)
		{
			cout << "Cannot open or find RuntimeUpdates.config - Generating new blank file.\n";
			goto ResetConfigFile;
		}


		//Epochs Per Interval
		inStream.getline(buffer,2048);
		line = buffer;
		Tokenize(line, tokens, "=");
		if(tokens.size() > 1)
		{
			EpochsPerInterval = VerifyValuePositive(atoi(tokens[1].c_str()));
			cout << "Runtime Update: EpochsPerInterval = " << EpochsPerInterval << endl;
			ResetConfigFile = true;
		}
		tokens.clear();

		//Intervals
		inStream.getline(buffer,2048);
		line = buffer;
		Tokenize(line, tokens, "=");
		if(tokens.size() > 1)
		{
			Intervals = VerifyValuePositive(atoi(tokens[1].c_str()));
			ResetConfigFile = true;
			cout << "Runtime Update: Intervals = " << Intervals << endl;
		}
		tokens.clear();

		//Rho
		inStream.getline(buffer,2048);
		line = buffer;
		Tokenize(line, tokens, "=");
		if(tokens.size() > 1)
		{
			Rho = VerifyValuePositive(atof(tokens[1].c_str()));
			ResetConfigFile = true;
			cout << "Runtime Update: Rho = " << Rho << endl;
		}
		tokens.clear();

		//Rho Multiplier
		inStream.getline(buffer,2048);
		line = buffer;
		Tokenize(line, tokens, "=");
		if(tokens.size() > 1)
		{
			RhoMultiplier = VerifyValuePositive(atof(tokens[1].c_str()));
			ResetConfigFile = true;
			cout << "Runtime Update: RhoMultiplier = " << RhoMultiplier << endl;
		}
		tokens.clear();

		//Dynamic Rho Interval
		inStream.getline(buffer,2048);
		line = buffer;
		Tokenize(line, tokens, "=");
		if(tokens.size() > 1)
		{
			RhoInterval = VerifyValuePositive(atoi(tokens[1].c_str()));
			ResetConfigFile = true;
			cout << "Runtime Update: RhoInterval = " << RhoInterval << endl;
		}
		tokens.clear();

		//Use Momentum (Disabled In This Version)
		inStream.getline(buffer,2048);
		line = buffer;
		Tokenize(line, tokens, "=");
		if(tokens.size() > 1)
		{
			//Disabled - Dont do anything right now
			ResetConfigFile = true;
			cout << "Haha, nice try. This functionality is disabled!" << endl;
		}
		tokens.clear();

		//Momentum Scaler (Disabled In This Version)
		inStream.getline(buffer,2048);
		line = buffer;
		Tokenize(line, tokens, "=");
		if(tokens.size() > 1)
		{
			//Disabled - Dont do anything right now
			ResetConfigFile = true;
			cout << "Haha, nice try. This functionality is disabled!" << endl;
		}
		tokens.clear();

		//Sim Anneal Max Percent
		inStream.getline(buffer,2048);
		line = buffer;
		Tokenize(line, tokens, "=");
		if(tokens.size() > 1)
		{
			SimAnnealMaxPercent = VerifyValuePositive(atoi(tokens[1].c_str()));
			ResetConfigFile = true;
			cout << "Runtime Update: SimAnnealMaxPercent = " << SimAnnealMaxPercent<< endl;
		}
		tokens.clear();

		//Sim Anneal Interval
		inStream.getline(buffer,2048);
		line = buffer;
		Tokenize(line, tokens, "=");
		if(tokens.size() > 1)
		{
			SimAnnealInterval = VerifyValuePositive(atoi(tokens[1].c_str()));
			ResetConfigFile = true;
			cout << "Runtime Update: SimAnnealInterval = " << SimAnnealInterval << endl;
		}
		tokens.clear();

		//Save Interval
		inStream.getline(buffer,2048);
		line = buffer;
		Tokenize(line, tokens, "=");
		if(tokens.size() > 1)
		{
			SaveInterval = VerifyValuePositive(atoi(tokens[1].c_str()));
			ResetConfigFile = true;
			cout << "Runtime Update: SaveInterval = " << SaveInterval << endl;
		}
		tokens.clear();

		//File Title
		inStream.getline(buffer,2048);
		line = buffer;
		Tokenize(line, tokens, "=");
		if(tokens.size() > 1)
		{
			FileTitle = tokens[1];
			ResetConfigFile = true;
			cout << "Runtime Update: FileTitle = " << FileTitle << endl;
		}
		tokens.clear();

		//Console Logging Interval
		inStream.getline(buffer,2048);
		line = buffer;
		Tokenize(line, tokens, "=");
		if(tokens.size() > 1)
		{
			ConsoleLoggingInterval = VerifyValuePositive(atoi(tokens[1].c_str()));
			ResetConfigFile = true;
			cout << "Runtime Update: ConsoleLoggingInterval = " << ConsoleLoggingInterval << endl;
		}
		tokens.clear();

		//Save Data Now
		inStream.getline(buffer,2048);
		line = buffer;
		Tokenize(line, tokens, "=");
		if(tokens.size() > 1)
		{
			SaveData();
			SavedData = 1;
			ResetConfigFile = true;
		}
		tokens.clear();

	}
	catch(...)
	{
		cout << "Error Reading RuntimeUpdate.config File!";
	}

	inStream.close();


	//////////////////////////////////////////
	//DOES CONFIG FILE NEED TO BE RESET?	//
	//////////////////////////////////////////

	if( ResetConfigFile == true )
	{
		//Reset the config file by re-outputting a blank config file
ResetConfigFile:

			ofstream file;
			file.open("RuntimeUpdates.config");
			try
			{
					file << "Epochs Per Interval=" << endl;
					file << "Intervals=" << endl;
					file << "Rho=" << endl;
					file << "Rho multiplier=" << endl;
					file << "Dynamic rho interval=" << endl;
					file << "Momentum=" << endl;
					file << "Momentum Scaler=" << endl;
					file << "Simulated annealing max percent(%)=" << endl;
					file << "Simulated annealing interval=" << endl;
					file << "Save Interval=" << endl;
					file << "File Title=" << endl;
					file << "Console Logging Interval" << endl;
					file << "Save Data Now=";
			}
			catch(...)
			{
				cout << "Exception Occurred Resetting RuntimeUpdate.config file! - Do you have the file open?";
			}
			file.close();
	}


	return SavedData;

}//RuntimeConfigUpdate()

double Backprop::VerifyValuePositive( double value )
{
	if(value == 0)
	{
		cout << "Inputted value found to be 0: Invalid Input Value. Setting to 1 as default" << endl;
		return 1;
	}
	else if( value < 0 )
	{
		cout << "Inputted value found to be negative: Invalid Input Value. Setting to positive as default" << endl;
		return -value;
	}
	else
		return value;
}