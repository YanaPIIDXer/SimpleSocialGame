require 'rails_helper'

RSpec.describe "Logins", type: :request do
  describe "Login" do
    it "Create User" do
      post "/login", params: {name: "test"}
      user = User.find_by(name: "test")
      expect(user).not_to eq nil
    end

    it "Null Request" do
      post "/login"
      expect(response).not_to have_http_status(:success)
    end

    it "Empty Name" do
      post "/login", params: {name: ""}
      expect(response).not_to have_http_status(:success)
    end
  end
end
