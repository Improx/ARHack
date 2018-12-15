const mongoose = require('mongoose')
require('mongoose-double')(mongoose)

const SchemaTypes = mongoose.Schema.Types;

const BoneTransform = new mongoose.Schema({
  rotation: {
    type: [SchemaTypes.Double],
    length: 4
  },
  position: {
    type: [SchemaTypes.Double],
    length: 3
  }
})


const Frame = new mongoose.Schema({
  bones: [BoneTransform],
})

const MessageSchema = new mongoose.Schema({
  text: String,
  location: {
    type: {
      type: String,
      enum: ['Point'],
      required: true
    },
    coordinates: {
      type: [SchemaTypes.Double],
      required: true
    }
  },
  altitude: {
    type: SchemaTypes.Double,
    required: true
  },
  animation: {
    type: [Frame],
    required: false,
    default: undefined
  },
  modelId: String,
  points: {
    type: Number,
    required: true,
    default: 0
  }
})

MessageSchema.index({location: '2dsphere'})

const Message = mongoose.model('Message', MessageSchema)

module.exports = Message