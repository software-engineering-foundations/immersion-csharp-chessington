# Chessington

With the rise in popularity of chess worldwide, it's time to take advantage of this. Your task is to build a chess application, with the snappy title of Chessington.

## Starting steps

When running the application (Chessington.UI) you should see a chess board! You can try dragging one of the white pieces to make a move. However, none of the logic to decide which moves are valid has been implemented yet. That's going to be your job.

## The red-green-refactor cycle

Test Driven Development (TDD) is a process for writing unit tests in lock-step with code. Here's the mantra:

1. Red – write a test which fails.
1. Green – write the simplest code that makes the test pass.
1. Refactor – try to refactor the code to make it beautiful, without breaking any tests.

Writing good unit tests is hard, so some have been written some for you. This will allow you to get used to the red-green-refactor cycle. By the end of the process, you can start writing your own tests.

## Steps to repeat

Run your unit tests and verify that some of them fail. Find the failing tests based on the information in the test report. Read the tests, and make sure you understand what they are testing.

Choose a failing test. Once you understand your test, write the simplest code that makes the test pass. Once your test passes, see if you can improve things by refactoring your code to make it nicer (without changing its functionality). This may not always be possible.

When you're happy, and the test is still green, run the program and try out the new functionality! Then commit your changes with a meaningful comment, and choose the next failing test.

Keep repeating this cycle until you have something beginning to look like a proper chess game.

## Writing your own tests

Now it's your turn to start writing your own tests. Choose a piece with some missing functionality and write a test for it. From looking at the previous tests, you should have all the ingredients you need to get started on your own.

Now run your test, see it fail, and make it pass. Continue this (remembering to commit frequently) until the piece is moving correctly. Continue until all the pieces are done.

While you're doing this, keep a look out for opportunities for refactoring.

## More rules

Chess is a game of difficult edge cases and weird behaviours. When all of your pieces move correctly in standard ways, try to implement some of this functionality:

- En Passant
- Castling
- Pawn Promotion
- Check and Check Mate
- Stalemate

## Stretch goal

Develop a chess AI. See if you can write a simple AI which plays the Black pieces. Start by picking a valid move at random.
