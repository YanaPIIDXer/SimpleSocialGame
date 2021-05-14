require 'rails_helper'

RSpec.describe "Logins", type: :request do
  describe "Login" do
    it "Create User" do
      post "/login", params: {name: "test"}
      user = User.find_by(name: "test")
      expect(user).not_to eq nil
    end
  end
end
