#include <string>
#ifndef RECORD_H
#define RECORD_H
struct record {
    std::string name;
    std::string address;
    int birth_year;
    std::string phone_number;
    struct record* next;
};
#endif
