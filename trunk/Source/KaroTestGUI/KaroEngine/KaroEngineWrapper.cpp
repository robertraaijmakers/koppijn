#include "StdAfx.h"
#include <msclr\marshal_cppstd.h>
#include "KaroEngineWrapper.h"


using namespace msclr::interop;
using namespace KaroEngine;

KaroEngineWrapper::KaroEngineWrapper(void)
{
	_karoEngine = new KaroEngine();	
}

KaroEngineWrapper::~KaroEngineWrapper(void)
{
	if(_karoEngine)
		delete _karoEngine;
}

Tile KaroEngineWrapper::GetByXY(int x, int y){
	return _karoEngine->GetByXY(x,y);
}

void KaroEngineWrapper::DoMove(int from, int to, int tile){	
	_karoEngine->DoMove(from,to,tile);
}

GameState KaroEngineWrapper::GetGameState(){
	return _karoEngine->GetGameState();
}

Player KaroEngineWrapper::GetTurn()
{
	return _karoEngine->GetTurn();
}

bool KaroEngineWrapper::InsertByXY(int x, int y) {
	int position=(y*_karoEngine->BOARDWIDTH)+x;
	return _karoEngine->InsertByXY(position);
}

int KaroEngineWrapper::GetEvaluationScore(){
	return _karoEngine->GetEvaluationScore();
}

void KaroEngineWrapper::CalculateComputerMove(){
	_karoEngine->CalculateComputerMove();
}

String ^KaroEngineWrapper::GetMessageLog(){
	return marshal_as<String ^>(_karoEngine->GetMessageLog());
}

array<array<int>^>^ KaroEngineWrapper::GetPossibleMoves(int x, int y,int tileFromX,int tileFromY){
	vector<Move*>* possibleMoves = _karoEngine->GetPossibleMoves((y*_karoEngine->BOARDWIDTH+x),false);
	
	int tilefrom=(tileFromY*_karoEngine->BOARDWIDTH+tileFromX);
	if(tileFromX == -1){
		tilefrom=-1;
	}

	int index=0;
	for(int i=0;i<possibleMoves->size();i++){
		if(possibleMoves->at(i)->tileFrom ==tilefrom){
			index++;
		}
	}
	array<array<int>^>^ params = gcnew array<array<int>^>(index);
	index=0;
	for(int i=0;i<possibleMoves->size();i++){
		if(possibleMoves->at(i)->tileFrom ==tilefrom){
			params[index]=gcnew array<int>(2);
			params[index][0] = possibleMoves->at(i)->positionTo%_karoEngine->BOARDWIDTH;
			params[index][1] = possibleMoves->at(i)->positionTo/_karoEngine->BOARDWIDTH;
			index++;
		}
	}
	return params;
}

void KaroEngineWrapper::UndoLastMove() {
	_karoEngine->UndoMove(_karoEngine->lastMove);
}