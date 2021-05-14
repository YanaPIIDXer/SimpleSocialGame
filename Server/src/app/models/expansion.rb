class Expansion < ApplicationRecord
    has_many :card, foreign_key: :expansion_id
end
