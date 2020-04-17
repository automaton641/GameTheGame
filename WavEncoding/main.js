const WavEncoder = require("wav-encoder");
const fs = require('fs')
try {
  const samplesString = fs.readFileSync("song.wave", "utf8");
  let numberOfSamples = 0;
  let sampleStrings = samplesString.split("\n");

  //console.log(sampleStrings[0]);

  
  let floatArray = new Float32Array(sampleStrings.length);
  for (let i=0; i < sampleStrings.length; i++) {
    floatArray[i] = parseFloat(sampleStrings[i]);
  }  
  const whiteNoise1sec = {
    sampleRate: 44100,
    channelData: [
      floatArray,
      floatArray
    ]
  };
   
  WavEncoder.encode(whiteNoise1sec).then((buffer) => {
    fs.writeFileSync("song.wav", new Buffer(buffer));
  });
} catch (err) {
  console.error(err);
}

