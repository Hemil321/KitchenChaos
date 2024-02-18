#include <iostream>
#include <string>
#include "address_book.h"
using namespace std;

int main(int argc, char *argv[]) {

    if (argc > 1) {
        cout << "Application does not take arguments." << endl;
        return -1;
    }
    #ifdef DEBUG
    {
        cout << "********************\n";
        cout << "**   DEBUG MODE   **\n";
    }
    #endif

    cout << "********************\n";
    cout << "Welcome User!\n";
    cout << "This is the Address Book App!\n";
    cout << "--------------------\n";
    run_program();
    cout << "--------------------\n";
    cout << "App terminated\n";
    cout << "********************\n";
    return 0;
}
