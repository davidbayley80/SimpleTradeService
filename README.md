# SimpleTradeService

Project: Trade batch simulator. Whereby trade details are read, processed, then placed and a OUT queue and/or saved to a database. 

Considerations: For the sake of this example I've used an input csv file as the 'Trade Source'. In practice, trades can either be read from a messaging queue (such as Middleware or Kafka) if they are required in real time, or database queries can be ran against the trade booking system DB at specified time to gather trade details. 

To-do: 

- Use Log4Net to capture exceptions and warnings
- Integrate Mango DB to allow saving and aggregation of previous results 
- Quant library to calculate Risk/PNL valuations and append to result set
- Security: Incoming file to be encrypted/decrypted using a simple public/private key mechanism 
