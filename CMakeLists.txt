# the minimum version this module was tested against
cmake_minimum_required(VERSION 3.8 FATAL_ERROR)

project(Party_Squares_Game CSharp)

set_property(DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR} PROPERTY VS_STARTUP_PROJECT Party_Squares_Game)

set(target_output_dir ${CMAKE_SOURCE_DIR}/Target/bin)

add_subdirectory(JSON-Parser)
add_subdirectory(PartySquaresGame)

message(STATUS "Output Dir: ${target_output_dir}")
file(COPY Data DESTINATION ${target_output_dir}/Debug)
file(COPY Data DESTINATION ${target_output_dir}/Release)

file(COPY Images DESTINATION ${target_output_dir}/Debug)
file(COPY Images DESTINATION ${target_output_dir}/Release)